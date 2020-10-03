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
where TYPE = 1 and LOW_DATEC > '01-01-2010' and GOAL = 0 and ( INS_DBT_V !=0 or ALL_PNY_V !=0 or  ALL_PNLT_V !=0    ) ;

results table "**_T1";

select
 REG_NUM  , // as 'REG_NUM',
LOW_DATEC   as '2_��������',
INS_DBT_V, // as '3_���',
DBT_PAYD_V , //as '4_���_����',
NEDZA , //as '5_���-����',
ALL_PNY_V, // as '6_����',
PNY_PAYD_V, // as '7_����_������_�����',
PENIZA , //as '8_����-����',
ALL_PNLT_V , //as '9_�����',
PNLT_PYD_V, // as '10_�����_������_�����',
CHTRAFZA , //as '11_�����-����',
AKT_NUM_V , //as '12_����_N',
AKT_DATE_V , //as '13_����_����',
WNT_NUM_V , //as '14_����_N' ,
WNT_DATE_V   //as '15_����_����'
from  _T1
where  NEDZA !=0 or  PENIZA != 0 or   CHTRAFZA !=0  ;