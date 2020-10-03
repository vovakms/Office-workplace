fix all;
select REG_NUM as '1_������',
LOW_DATEC as '2_��������',
INS_DBT_V as '3_���',
DBT_PAYD_V as '4_���',
DBT_FGVD_V as '5_���',
ALL_PNY_V as '6_����',
PNY_PAYD_V as '7_����',
PNY_FGVD_V as '8_����',
ALL_PNLT_V as '9_�����',
PNLT_PYD_V as '10_�����',
PNLT_FVD_V as '11_�����',
AKT_NUM
from  RCHECK
where TYPE = 1 and LOW_DATEC > '01-01-2010' and GOAL = 0 and ( INS_DBT_V !=0 or ALL_PNY_V !=0 or  ALL_PNLT_V !=0 ) ;