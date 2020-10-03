var @g='2016';
var @k='1';

fix all;
select
char(REG_NUM,10) as '1_РегНом' ,
LOW_DATEC  as '2_Дата',
S_TOTDEBT  as '3_недЗадолжн',    //нед    Всего задолженность
S_IDBTPD as '4_недПолуч' , // нед   получено
(S_TOTDEBT-S_IDBTPD ) as '5_НедРазн',
S_FINECLC   as '6_ПениНач'    , //пен  начислено
S_FINPAYD as '7_пениПолучено',  //пен   получено
(S_FINECLC -S_FINPAYD ) as '8_ПениРазница',
S_PNLTCLC  as '9_штрафНач'  , //начислено: штрафы,
S_PNLPAYD  as '10_штрПолуч'  , // штрафы, получено
(S_PNLTCLC - S_PNLPAYD) as '11_ШтрафРазница'
from  RCHECK
where  TYPE = 1 and LOW_DATEC > '01-01-2010' and ( S_TOTDEBT !=0 or  S_FINECLC !=0 or  S_PNLTCLC !=0  )  ;  //           