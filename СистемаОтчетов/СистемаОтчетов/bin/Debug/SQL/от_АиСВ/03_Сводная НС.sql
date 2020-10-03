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
char(REG_NUM,10) as 'REG_NUM'  , // as 'REG_NUM',
LOW_DATEC , //as '2_ДатаПров',
INS_DBT_V, // as '3_Нед',
DBT_PAYD_V , //as '4_Нед_зачт',
NEDZA , //as '5_Нед-зачт',
ALL_PNY_V, // as '6_Пени',
PNY_PAYD_V, // as '7_Пени_зачтНС_переч',
PENIZA , //as '8_пени-зачт',
ALL_PNLT_V , //as '9_Штраф',
PNLT_PYD_V, // as '10_Штраф_зачтНС_переч',
CHTRAFZA , //as '11_штраф-зачт',
AKT_NUM_V , //as '12_докл_N',
AKT_DATE_V , //as '13_докл_дата',
WNT_NUM_V , //as '14_треб_N' ,
WNT_DATE_V   //as '15_треб_дата'
from  _T1
where  NEDZA !=0 or  PENIZA != 0 or   CHTRAFZA !=0  ;

results table "**_T2";
 
select REG_NUM, NAME
from RCHKWRK
group by REG_NUM   ;

results table "**_tab1" ;

select char(F4INFO.REG_NUM,10) as 'REG_NUM', F4INFO.NAME as 'NAME', _tab1.NAME as 'UPOLN', F4INFO.T7R39C1 as 'SUMMA'
from   F4INFO, _tab1
where    F4INFO.T7R39C1 !=0 and _tab1.REG_NUM  = F4INFO.ID_RPR and F4INFO.YEAR_NUM = @g and F4INFO.QUART_NUM = @k   ;

results table "**_tab2" ;
 
select 
ta.REG_NUM as 'РегНом'  , // as 'REG_NUM',
ta.LOW_DATEC  as '2_ДатаПров',
ta.INS_DBT_V  as '3_Нед',
ta.DBT_PAYD_V , //as '4_Нед_зачт',
ta.NEDZA  as '5_Нед-зачт',
ta.ALL_PNY_V  as '6_Пени',
ta.PNY_PAYD_V  as '7_Пени_зачтНС_переч',
ta.PENIZA   '8_пени-зачт',
ta.ALL_PNLT_V   '9_Штраф',
ta.PNLT_PYD_V  as '10_Штраф_зачтНС_переч',
ta.CHTRAFZA  as '11_штраф-зачт',
ta.AKT_NUM_V as '12_докл_N',
ta.AKT_DATE_V  as '13_докл_дата',
ta.WNT_NUM_V  as '14_треб_N' ,
ta.WNT_DATE_V  as '15_треб_дата',
tb.REG_NUM as '16_РегНом',
tb.NAME as '17_НаимОрг',
tb.UPOLN as '18_Уполн',
tb.SUMMA as '19_СУММА'
from _T2 ta , _tab2 tb where tb.REG_NUM =  ta.REG_NUM     ;



