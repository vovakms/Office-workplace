var @g='2016';
var @k='1';

fix all;
select
char(REG_NUM,10) as 'REG_NUM' ,
LOW_DATEC ,
INS_DBT_V  ,
DBT_PAYD_V  ,
(INS_DBT_V - DBT_PAYD_V ) as 'NEDZA',
ALL_PNY_V  ,
PNY_PAYD_V  ,
(ALL_PNY_V - PNY_PAYD_V) as 'PENIZA',
ALL_PNLT_V  ,
PNLT_PYD_V  ,
(ALL_PNLT_V - PNLT_PYD_V) as 'CHTRAFZA',
AKT_NUM_V  ,
AKT_DATE_V  ,
WNT_NUM_V   ,
WNT_DATE_V
from  RCHECK
where TYPE = 1 and LOW_DATEC > '01-01-2010' and GOAL = 0 and ( INS_DBT_V !=0 or ALL_PNY_V !=0 or  ALL_PNLT_V !=0 );//  

results table "**_T1";
 
select
REG_NUM as '1_РегНом',
LOW_DATEC as '2_ДатаПров',
INS_DBT_V as '3_Нед',
DBT_PAYD_V as '4_Нед_зачт',
NEDZA as '5_Нед-зачт',
ALL_PNY_V as '6_Пени',
PNY_PAYD_V as '7_Пени_зачтНС_переч',
PENIZA as '8_пени-зачт',
ALL_PNLT_V as '9_Штраф',
PNLT_PYD_V as '10_Штраф_зачтНС_переч',
CHTRAFZA as '11_штраф-зачт',
AKT_NUM_V as '12_докл_N',
AKT_DATE_V as '13_докл_дата',
WNT_NUM_V as '14_треб_N' ,
WNT_DATE_V as '15_треб_дата'
from  _T1 ; // where  NEDZA !=0 or  PENIZA != 0 or   CHTRAFZA !=0  ;