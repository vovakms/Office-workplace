var @g='2016';
var @k='1';

fix all;

select REG_NUM, NAME
from RCHKWRK
group by REG_NUM   ;

results table "**_tab1" ;

select 
char(F4INFO.REG_NUM,10) as 'РегНом',
F4INFO.NAME as 'Наименование страхователя' ,
_tab1.NAME as 'Уполн', 
F4INFO.T7R39C1 as 'Сумма', 
F4INFO.YEAR_NUM as 'Год', 
F4INFO.QUART_NUM as 'квартал', 
F4INFO.CRE_DATE as 'Дата сдачи'
from   F4INFO, _tab1
where    F4INFO.T7R39C1 !=0 and _tab1.REG_NUM  = F4INFO.ID_RPR and F4INFO.YEAR_NUM = @g and F4INFO.QUART_NUM = @k ;     //