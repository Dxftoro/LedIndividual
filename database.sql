CREATE SEQUENCE public.product_id_seq;

CREATE TABLE public.Product (
                ID INTEGER NOT NULL DEFAULT nextval('public.product_id_seq'),
                name VARCHAR(64) DEFAULT ' ' NOT NULL,
                ed VARCHAR(32) DEFAULT 'pcs' NOT NULL,
                CONSTRAINT pk_product PRIMARY KEY (ID)
);


ALTER SEQUENCE public.product_id_seq OWNED BY public.Product.ID;

CREATE SEQUENCE public.price_list_id_seq;

CREATE TABLE public.Price_List (
                ID INTEGER NOT NULL DEFAULT nextval('public.price_list_id_seq'),
                id_product INTEGER NOT NULL,
                price INTEGER DEFAULT 0 NOT NULL,
                CONSTRAINT pk_price_list PRIMARY KEY (ID)
);

ALTER SEQUENCE public.price_list_id_seq OWNED BY public.Price_List.ID;

CREATE SEQUENCE public.client_id_seq;

CREATE TABLE public.Client (
                ID INTEGER NOT NULL DEFAULT nextval('public.client_id_seq'),
                fio VARCHAR(64) DEFAULT ' ' NOT NULL,
                phone VARCHAR(32) DEFAULT '+0000' NOT NULL,
                address VARCHAR(256) DEFAULT ' ' NOT NULL,
                CONSTRAINT pk_client PRIMARY KEY (ID)
);


ALTER SEQUENCE public.client_id_seq OWNED BY public.Client.ID;

CREATE SEQUENCE public.order_1_id_seq;

CREATE TABLE public.Order_1 (
                id INTEGER NOT NULL DEFAULT nextval('public.order_1_id_seq'),
                id_client INTEGER NOT NULL,
                order_date DATE NOT NULL,
                delivery_date DATE,
                total_sum INTEGER DEFAULT 0 NOT NULL,
                CONSTRAINT pk_order_1 PRIMARY KEY (id)
);

ALTER SEQUENCE public.order_1_id_seq OWNED BY public.Order_1.id;

CREATE SEQUENCE public.order_info_id_seq;

CREATE TABLE public.Order_Info (
                ID INTEGER NOT NULL DEFAULT nextval('public.order_info_id_seq'),
                id_price INTEGER NOT NULL,
                id_order INTEGER NOT NULL,
                quantity INTEGER DEFAULT 0 NOT NULL,
                CONSTRAINT pk_order_info PRIMARY KEY (ID)
);

ALTER SEQUENCE public.order_info_id_seq OWNED BY public.Order_Info.ID;

CREATE OR REPLACE FUNCTION insert_order_info() RETURNS TRIGGER AS $ad_oi_trigger$
	BEGIN
		UPDATE Order_1
		SET total_sum = total_sum + NEW.quantity * (
			SELECT price FROM Price_List WHERE ID = NEW.id_price
		)
		WHERE Order_1.id = NEW.id_order;
		RETURN NULL;
	END
$ad_oi_trigger$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION update_order_info() RETURNS TRIGGER AS $up_oi_trigger$
	BEGIN
		UPDATE Order_1
		SET total_sum = total_sum + (NEW.quantity - OLD.quantity) * (
			SELECT price FROM Price_List WHERE ID = NEW.id_price
		)
		WHERE Order_1.id = NEW.id_order;
		RETURN NULL;
	END
$up_oi_trigger$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION delete_order_info() RETURNS TRIGGER AS $del_oi_trigger$
	BEGIN
		UPDATE Order_1
		SET total_sum = total_sum - OLD.quantity * (
			SELECT price FROM Price_List WHERE ID = OLD.id_price
		)
		WHERE Order_1.id = OLD.id_order;
		RETURN NULL;
	END
$del_oi_trigger$ LANGUAGE plpgsql;

CREATE TRIGGER ins_order_info AFTER INSERT ON Order_Info
	FOR EACH ROW EXECUTE PROCEDURE insert_order_info();

CREATE TRIGGER upd_order_info AFTER UPDATE ON Order_Info
	FOR EACH ROW EXECUTE PROCEDURE update_order_info();

CREATE TRIGGER del_order_info AFTER DELETE ON Order_Info
	FOR EACH ROW EXECUTE PROCEDURE delete_order_info();

ALTER TABLE public.Price_List ADD CONSTRAINT product_price_list_fk
FOREIGN KEY (id_product)
REFERENCES public.Product (ID)
ON DELETE CASCADE
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Order_Info ADD CONSTRAINT price_list_order_info_fk
FOREIGN KEY (id_price)
REFERENCES public.Price_List (ID)
ON DELETE RESTRICT
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Order_1 ADD CONSTRAINT client_order_fk
FOREIGN KEY (id_client)
REFERENCES public.Client (ID)
ON DELETE CASCADE
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.Order_Info ADD CONSTRAINT order_order_info_fk
FOREIGN KEY (id_order)
REFERENCES public.Order_1 (id)
ON DELETE CASCADE
ON UPDATE NO ACTION
NOT DEFERRABLE;

-- Named order table view
create view Named_Order as
select
	order_1.id,
	client.fio,
	order_1.order_date,
	order_1.delivery_date,
	order_1.total_sum
from order_1
left join Client
on order_1.id_client = client.id;

-- Named order info table view
create view Named_Order_Info as
select
	info.id,
	ord.id as id_order,
	prod.name,
	plist.price,
	info.quantity,
	client.fio
from Order_Info as info
left join Price_List as plist
on info.id_price = plist.id
left join Product as prod
on plist.id_product = prod.id
left join Order_1 as ord
on info.id_order = ord.id
left join Client
on ord.id_client = client.id;

-- Named price list table view
create view Named_Price_List as
select
	price_list.id,
	product.name,
	price_list.price
from Price_List
join Product
on price_list.id_product = product.id;

INSERT INTO public.Product (name, ed) VALUES
('Клавиатура механическая', 'шт'),
('Мышь оптическая', 'шт'),
('Монитор 24"', 'шт'),
('Системный блок', 'шт'),
('Наушники игровые', 'шт'),
('Веб-камера HD', 'шт'),
('Коврик для мыши', 'шт'),
('USB-флешка 64GB', 'шт'),
('Внешний HDD 1TB', 'шт'),
('Принтер лазерный', 'шт');

INSERT INTO public.Price_List (id_product, price) VALUES
(1, 3500),   -- Клавиатура механическая
(2, 1200),   -- Мышь оптическая
(3, 15000),  -- Монитор 24"
(4, 25000),  -- Системный блок
(5, 2800),   -- Наушники игровые
(6, 1800),   -- Веб-камера HD
(7, 350),    -- Коврик для мыши
(8, 800),    -- USB-флешка 64GB
(9, 4200),   -- Внешний HDD 1TB
(10, 12000); -- Принтер лазерный

INSERT INTO public.Client (fio, phone, address) VALUES
('Иванов Иван Иванович', '+79123456789', 'г. Москва, ул. Ленина, д. 10, кв. 15'),
('Петрова Анна Сергеевна', '+79234567890', 'г. Санкт-Петербург, пр. Невский, д. 25, кв. 42'),
('Сидоров Алексей Владимирович', '+79345678901', 'г. Новосибирск, ул. Советская, д. 5, кв. 8'),
('Козлова Екатерина Дмитриевна', '+79456789012', 'г. Екатеринбург, ул. Малышева, д. 30, кв. 121'),
('Морозов Дмитрий Петрович', '+79567890123', 'г. Казань, ул. Баумана, д. 15, кв. 7'),
('Волкова Ольга Александровна', '+79678901234', 'г. Нижний Новгород, ул. Большая Покровская, д. 8, кв. 56'),
('Соколов Павел Игоревич', '+79789012345', 'г. Челябинск, ул. Кирова, д. 42, кв. 33'),
('Михайлова Татьяна Владимировна', '+79890123456', 'г. Омск, ул. Ленина, д. 17, кв. 9'),
('Федоров Андрей Николаевич', '+79901234567', 'г. Ростов-на-Дону, ул. Пушкинская, д. 3, кв. 18'),
('Николаева Мария Павловна', '+79012345678', 'г. Уфа, ул. Цюрупы, д. 22, кв. 45');

INSERT INTO public.Order_1 (id_client, order_date, delivery_date, total_sum) VALUES
(1, '2026-01-15', '2026-01-20', 4700),   -- Иванов: клавиатура + мышь
(2, '2026-01-20', '2026-01-25', 15000),  -- Петрова: монитор
(3, '2026-02-01', '2026-02-05', 27800),  -- Сидоров: системный блок + наушники
(4, '2026-02-10', '2026-02-15', 350),    -- Козлова: коврик
(5, '2026-02-18', NULL, 5900),            -- Морозов: веб-камера + флешка (доставка ещё не выполнена)
(1, '2026-03-01', '2026-03-06', 4200),   -- Иванов: внешний HDD
(6, '2026-03-10', '2026-03-15', 37000),  -- Волкова: системный блок + монитор
(7, '2026-03-15', '2026-03-20', 2800),   -- Соколов: наушники
(8, '2026-03-20', NULL, 12000),          -- Михайлова: принтер (ждёт доставку)
(9, '2026-03-25', '2026-03-30', 4700),   -- Федоров: клавиатура + мышь
(10, '2026-04-01', '2026-04-05', 3500),  -- Николаева: клавиатура
(3, '2026-04-05', '2026-04-10', 800),    -- Сидоров: флешка
(5, '2026-04-08', NULL, 25000),          -- Морозов: системный блок (ждёт)
(2, '2026-04-10', '2026-04-15', 1800),   -- Петрова: веб-камера
(6, '2026-04-12', '2026-04-17', 1200);   -- Волкова: мышь

INSERT INTO public.Order_Info (id_price, id_order, quantity) VALUES
(1, 1, 1),   -- Заказ 1 (Иванов): клавиатура
(2, 1, 1),   -- Заказ 1 (Иванов): мышь
(3, 2, 1),   -- Заказ 2 (Петрова): монитор
(4, 3, 1),   -- Заказ 3 (Сидоров): системный блок
(5, 3, 1),   -- Заказ 3 (Сидоров): наушники
(7, 4, 2),   -- Заказ 4 (Козлова): коврик x2
(6, 5, 1),   -- Заказ 5 (Морозов): веб-камера
(8, 5, 2),   -- Заказ 5 (Морозов): флешка x2
(9, 6, 1),   -- Заказ 6 (Иванов): внешний HDD
(4, 7, 1),   -- Заказ 7 (Волкова): системный блок
(3, 7, 1),   -- Заказ 7 (Волкова): монитор
(5, 8, 2),   -- Заказ 8 (Соколов): наушники x2
(10, 9, 1),  -- Заказ 9 (Михайлова): принтер
(1, 10, 1),  -- Заказ 10 (Федоров): клавиатура
(2, 10, 1),  -- Заказ 10 (Федоров): мышь
(1, 11, 1),  -- Заказ 11 (Николаева): клавиатура
(8, 12, 1),  -- Заказ 12 (Сидоров): флешка
(4, 13, 1),  -- Заказ 13 (Морозов): системный блок
(6, 14, 1),  -- Заказ 14 (Петрова): веб-камера
(2, 15, 1);  -- Заказ 15 (Волкова): мышь

SELECT 'Product' AS table_name, COUNT(*) FROM public.Product
UNION ALL
SELECT 'Price_List', COUNT(*) FROM public.Price_List
UNION ALL
SELECT 'Client', COUNT(*) FROM public.Client
UNION ALL
SELECT 'Order_1', COUNT(*) FROM public.Order_1
UNION ALL
SELECT 'Order_Info', COUNT(*) FROM public.Order_Info;
SELECT SUM(total_sum) AS total_revenue FROM public.Order_1;

-- 3. Заказы с просроченной доставкой (delivery_date NULL и дата заказа старше 7 дней)
SELECT o.id, c.fio, o.order_date 
FROM public.Order_1 o
JOIN public.Client c ON o.id_client = c.id
WHERE o.delivery_date IS NULL 
  AND o.order_date < CURRENT_DATE - INTERVAL '7 days';

-- 4. Самый популярный товар по количеству продаж
SELECT p.name, SUM(oi.quantity) AS total_quantity
FROM public.Order_Info oi
JOIN public.Price_List pl ON oi.id_price = pl.id
JOIN public.Product p ON pl.id_product = p.id
GROUP BY p.name
ORDER BY total_quantity DESC
LIMIT 5;