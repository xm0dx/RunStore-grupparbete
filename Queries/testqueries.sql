-- testfrågor för att se att reglerna i databasen fungerar

SELECT * FROM suppliers;

-- ska ge fel, Nike finns redan (unikt index på suppliername)
INSERT INTO suppliers(suppliername)
VALUES('Nike');

-- ska ge fel, check constraint säger att price >= 0
INSERT INTO products(itemnumber, productname, price, supplier_id)
VALUES('1020', 'Test produkt', -100, 1);

INSERT INTO products(itemnumber, productname, price, supplier_id)
VALUES('1020', 'Test produkt', 100, 1), ('1021', 'Test produkt 2', 100, 2);

SELECT * FROM products;

INSERT INTO customers(firstname, lastname, email)
VALUES
('Michael', 'Gustavsson', 'michael@mail.com'),
('Emma', 'Olsson', 'emma@mail.com');

SELECT * FROM customers;

-- adresstyper
INSERT INTO addresstypes(addresstype) VALUES('Billing'), ('Delivery'), ('Office');
SELECT * FROM addresstypes;

-- orter och postnummer
INSERT INTO cities(city) VALUES('Stenungsund'), ('Göteborg'), ('Kungälv');
SELECT * FROM cities;

INSERT INTO postalcodes(postalcode, city_id) VALUES('44431', 1), ('44432', 3), ('44445', 1);
SELECT * FROM postalcodes
INNER JOIN cities ON postalcodes.city_id = cities.id;

-- adresser, varje adress har ett postnummer och en adresstyp
INSERT INTO addresses(addressline, postalcode_id, addresstype_id)
VALUES('Strandvägen 1', 1, 1), ('Storgatan 10', 1, 2), ('Hjälmaregatan 14', 2, 1);

SELECT * FROM addresses
INNER JOIN postalcodes ON addresses.postalcode_id = postalcodes.id
INNER JOIN cities ON postalcodes.city_id = cities.id
INNER JOIN addresstypes ON addresses.addresstype_id = addresstypes.id;

-- kopplingstabellen mellan kunder och adresser
INSERT INTO customeraddresses(customer_id, address_id)
VALUES(1, 1), (1, 3), (2, 1);

SELECT firstname, lastname, addressline, postalcode, city
FROM customeraddresses
INNER JOIN customers ON customers.id = customeraddresses.customer_id
INNER JOIN addresses ON addresses.id = customeraddresses.address_id
INNER JOIN postalcodes ON addresses.postalcode_id = postalcodes.id
INNER JOIN cities ON postalcodes.city_id = cities.id;

-- kopplingstabellen mellan leverantörer och adresser
INSERT INTO supplieraddresses(supplier_id, address_id)
VALUES(1, 1);

SELECT suppliername, addressline, addresstype
FROM supplieraddresses
INNER JOIN suppliers ON suppliers.id = supplieraddresses.supplier_id
INNER JOIN addresses ON addresses.id = supplieraddresses.address_id
INNER JOIN addresstypes ON addresses.addresstype_id = addresstypes.id;

-- orderdate sätts av databasen (CURRENT_TIMESTAMP)
INSERT INTO salesorders(ordernumber, customer_id)
VALUES
('1000', 1),
('1001', 1),
('1002', 1),
('1003', 2),
('1004', 2);

SELECT ordernumber, orderdate, firstname, lastname, email
FROM salesorders
INNER JOIN customers ON salesorders.customer_id = customers.id;

-- orderrader
INSERT INTO orderitems(order_id, product_id, quantity, price)
VALUES(1, 2, 1, 995);

INSERT INTO orderitems(order_id, product_id, quantity, price)
VALUES(2, 1, 1, 2995), (2, 2, 2, 1595);

SELECT ordernumber, firstname, lastname, productname, quantity, orderitems.price
FROM orderitems
INNER JOIN salesorders ON orderitems.order_id = salesorders.id
INNER JOIN customers ON salesorders.customer_id = customers.id
INNER JOIN products ON orderitems.product_id = products.id;

-- fakturor, en order kan ha flera fakturor
INSERT INTO invoicetypes(invoicetype) VALUES('Faktura'), ('Kreditfaktura');

INSERT INTO invoices(invoicenumber, duedate, invoicetype_id, order_id)
VALUES(5000, CURRENT_TIMESTAMP + INTERVAL '30 days', 1, 1);

SELECT invoicenumber, invoicedate, duedate, invoicetype, ordernumber
FROM invoices
INNER JOIN invoicetypes ON invoices.invoicetype_id = invoicetypes.id
INNER JOIN salesorders ON invoices.order_id = salesorders.id;

-- ska ge fel, restrict: ordern har orderrader och en faktura
DELETE FROM salesorders WHERE id = 1;
