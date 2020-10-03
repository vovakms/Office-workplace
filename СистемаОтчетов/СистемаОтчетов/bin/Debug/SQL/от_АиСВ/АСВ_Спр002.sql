var @g='2015';
var @k='3';

fix all;

select REG_NUM, NAME
from RCHKWRK
group by REG_NUM   ;

results table "**_tab1" ;

select char(F4INFO.REG_NUM,10), F4INFO.NAME  , _tab1.NAME , F4INFO.T7R39C1, F4INFO.YEAR_NUM, F4INFO.QUART_NUM, F4INFO.CRE_DATE
from   F4INFO, _tab1
where    F4INFO.T7R39C1 !=0 and _tab1.REG_NUM  = F4INFO.ID_RPR and F4INFO.YEAR_NUM = @g and F4INFO.QUART_NUM = @k ;     //