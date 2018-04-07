exec tSQLt.NewTestClass 'test_FusionPropertyOfIterators';
go

create procedure test_FusionPropertyOfIterators.[test Given collection of data it must procedure the value expected]
as
begin
    declare @actual decimal(8,2);

    select @actual = (
      select distinct
        sum(price * quantity) over (partition by zip)
        from (
          values
            (53202, 1.89, 3),
            (60191, 1.99, 2),
            (60060, 0.99, 7),
            (53202, 1.29, 8),
            (60191, 1.89, 2),
            (53202, 0.99, 3)
        ) as orders(zip, price, quantity)
        where zip = 53202
    );

    exec tSQLt.AssertEquals 18.96, @actual;
end;
go

exec tSQLt.Run 'test_FusionPropertyOfIterators';
go

exec tSQLt.DropClass 'test_FusionPropertyOfIterators';
go