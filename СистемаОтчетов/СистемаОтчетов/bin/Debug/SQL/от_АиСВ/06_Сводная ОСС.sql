var @g='2016';
var @k='1';

fix all;
select
char(REG_NUM,10) as 'REG_NUM',// as '1_РегНом' ,
LOW_DATEC ,// as '2_Дата',
S_TOTDEBT ,// as '3_недЗадолжн',    //нед    Всего задолженность
S_IDBTPD,// as '4_недПолуч' , // нед   получено
(S_TOTDEBT-S_IDBTPD ) as 'NEDRAZ',// as '5_НедРазн',
S_FINECLC  ,// as '6_ПениНач'    , //пен  начислено
S_FINPAYD ,//as '7_пениПолучено',  //пен   получено
(S_FINECLC -S_FINPAYD ) as 'PENRAZ',//as '8_ПениРазница',
S_PNLTCLC ,// as '9_штрафНач'  , //начислено: штрафы,
S_PNLPAYD ,// as '10_штрПолуч'  , // штрафы, получено
(S_PNLTCLC - S_PNLPAYD) as 'SCHTRRAZ' // as '11_ШтрафРазница',
from  RCHECK
where  TYPE = 1 and LOW_DATEC > '01-01-2010' and ( S_TOTDEBT !=0 or  S_FINECLC !=0 or  S_PNLTCLC !=0  )  ;

results table "**_T1";

select
char(F4INF1.REG_NUM,10) as 'REG_NUM',// as 'РегНом',
F4INF1.S2R18        ,//  as 'Сумма',
F4INF1.YEAR_NUM ,//as 'Год',
F4INF1.QUART_NUM  // as 'квартал'
from   F4INF1
where    F4INF1.S2R18 !=0   and F4INF1.YEAR_NUM = @g and F4INF1.QUART_NUM = @k ;

results table "**_T2" ;

select
ta.REG_NUM   as 'РегНом' ,
ta.LOW_DATEC   as '2_Дата',
ta.S_TOTDEBT  as '3_недЗадолжн',    //нед    Всего задолженность
ta.S_IDBTPD  as '4_недПолуч' , // нед   получено
ta.NEDRAZ   as '5_НедРазн',
ta.S_FINECLC   as '6_ПениНач'    , //пен  начислено
ta.S_FINPAYD  as '7_пениПолучено',  //пен   получено
ta.PENRAZ  as '8_ПениРазница',
ta.S_PNLTCLC   as '9_штрафНач'  , //начислено: штрафы,
ta.S_PNLPAYD   as '10_штрПолуч'  , // штрафы, получено
ta.SCHTRRAZ  as '11_ШтрафРазница',
tb.REG_NUM  as 'РегНом',
tb.S2R18        as 'Сумма',
tb.YEAR_NUM  as 'Год',
tb.QUART_NUM   as 'квартал'
from _T1 ta , _T2 tb where ta.REG_NUM =  tb.REG_NUM     ;
