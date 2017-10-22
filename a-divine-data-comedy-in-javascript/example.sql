-- http://sqlfiddle.com/#!6/50442/6

create table stock_price (
   symbol varchar(5) not null
  ,price_date date not null
  ,price money not null
);

truncate table stock_price;

insert into stock_price
(symbol, price_date, price)
values
 ('ZVZZT', '2017-01-03', 10.02)
,('CBO'  , '2017-01-03', 18.99)
,('ZVZZT', '2017-01-04',  9.99)
,('CBO'  , '2017-01-04', 19.01)
;

select symbol, price
  from stock_price
  where symbol = 'ZVZZT'
  order by price_date
;

select max(price_date) as price_date
  from stock_price
;

select symbol, max(price_date) as price_date
  from stock_price
  group by symbol
;

select
   symbol
  ,max(price_date) over (
     partition by symbol) as price_date
  from stock_price
;

select
   symbol
  ,price
  ,lag(price, 1) over (
     partition by symbol
     order by price_date) as prev_price
  ,price - lag(price, 1) over (
     partition by symbol
     order by price_date) as price_change
  ,lead(price, 1) over (
    partition by symbol
    order by price_date) as next_price
  from stock_price
;