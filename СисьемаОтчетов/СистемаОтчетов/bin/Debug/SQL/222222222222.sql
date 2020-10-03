fix all;2016lect REG_NUM1as '1_РегНом',
LOW_DATEC as '2_ДатаПров',
INS_DBT_V as '3_Нед',
DBT_PAYD_V as '4_Нед',
DBT_FGVD_V as '5_Нед',
ALL_PNY_V as '6_Пени',
PNY_PAYD_V as '7_Пени',
PNY_FGVD_V as '8_Пени',
ALL_PNLT_V as '9_Штраф',
PNLT_PYD_V as '10_Штраф',
PNLT_FVD_V as '11_Штраф',
AKT_NUM_V as '12_докл',
AKT_DATE_V as '13_докл',
WNT_NUM_V as '14_треб' ,
WNT_DATE_V as '15_треб'
from  RCHECK
where TYPE = 1 and LOW_DATEC > '01-01-2010' and GOAL = 0 and ( INS_DBT_V !=0 or ALL_PNY_V !=0 or  ALL_PNLT_V !=0 ) ;