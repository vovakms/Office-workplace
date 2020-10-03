var @g='2016';
var @k='1';

fix all;

select 
char(F4INF1.REG_NUM,10) as 'РегНом',
F4INF1.S2R18           as 'Сумма', 
F4INF1.YEAR_NUM as 'Год', 
F4INF1.QUART_NUM as 'квартал' 
from   F4INF1 
where    F4INF1.S2R18 !=0   and F4INF1.YEAR_NUM = @g and F4INF1.QUART_NUM = @k ;     //