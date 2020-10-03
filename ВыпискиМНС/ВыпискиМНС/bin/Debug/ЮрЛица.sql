var @dat1='01-09-2016';
var @dat2='28-09-2016';
var @p1='';
var @p2='';

fix all; 
select
ID                                  ,// dwordsurrogate ,// Идентификатор записи
char(rtrim(INN, 'C'), 12) as 'INN'  ,// array(6) key  ,// ИНН юр.лица
KPP   ,// dword nonkey    ,// КПП юр.лица
OK    ,// byte key  ,// Информация по юр.лицам, импортированным из МСН
   // Признак готовности, переноса информации в EIP
char(rtrim(SENTTO, 'C'), 12) as 'SENTTO'     ,//array(5)  key   ,// Информация перенесена на карточку страхователя с рег.номером ...
SNT_DATE  ,//date key  ,// Дата переноса данных на карточку страхователя
SNT_TIME  ,//dword key  ,// Время переноса
XMLID,//dword key  ,// XIHIST.SRG Идентификатор файла XML, из которого импортирована запись
IDUL ,//dword key  ,// Идентификатор записи в импортированном файле
XMLNAME   ,//char(32) nonkey    ,// имя файла выписки
XMLDATE   ,//date nonkey    ,// дата импорта файла
EFLAG,//byte key  ,// признак наличия ошибок в выписке
//INDIVID   ,//long key   // признак ИЧП > 0
//IND_TYPE  ,//char(64) nonkey // Тип ИЧП:
   // "индивидуальный предприниматель",
   // "глава КФХ" и т.п.
   // пока нет справочника - текстом...
//IND_STAT  ,//dwordkey  ,// Статус ИЧП
//IND_RDAT  ,//date key  ,// Дата внесения записи (для ЕГРИП)
STATUS    ,//dwordkey  ,// Статус юр.лица
   // (ликвидировано),
   // банкрот и т.п.
CR_DATE  ,//date nonkey    ,// Дата регистрации юр.лица
END_DATE ,//date nonkey    ,// Окончание регистрации юр.лица
OKOPF    ,//dwordnonkey    ,// ОКОПФ юр.лица
OKVED    ,// char(8)   nonkey    ,// 
char(rtrim(REG_NUM, 'C'), 12) as 'REG_NUM' ,// array(5)  key    ,// Рег.номер страхователя (ФСС).
KPS_NUM  ,//word key  ,// КПС страхователя
REG_DATE ,//date nonkey    ,// Дата регистрации страхователя в ФСС
IN_DATE  ,//date nonkey    ,// Дата постановки страхователя на учет в исп. органе ФСС
OUT_DATE ,//date nonkey    ,// Дата снятия страхователя с учета в исп. органе ФСС
FSS_CODE ,//char(10)  nonkey    ,// Код подразделения ФСС, в котором страхователь состоит на учете
FSS_NAME ,//char(200) nonkey    ,// Название исполнительного органа ФСС, в котором страхователь состоит на учете
OGRN ,//char(15)  key  ,// ОГРН юр.лица
CR_NAME   ,//char(140) nonkey    ,// Наименование организации, осуществившей регистрацию юр.лица
NAME ,//char(255) nonkey    ,// Полное название юр.лица
ABBR ,//char(80)  nonkey    ,// краткое название юр.лица
ZIP  ,//char(6)   nonkey    ,// Индекс
CADDR,//char(128) nonkey    ,// Адрес
DDOC ,// date nonkey    ,// дата выдачи паспорта
NDOC ,// char(6)   nonkey    ,// номер паспорта
SDOC ,// char(10)  nonkey    ,// серия паспорта
ODOC ,// char(80)  nonkey    ,// кем выдан паспорт
BRT_DATE  ,//date nonkey    ,// дата рождения
BRT_PLC   ,//char(200) nonkey    ,// место рождения
DTSTART   ,//date key  ,// Дата внесения записи
DTSTNAME  ,//date key  ,// Дата внесения записи Наименование
DTSTOKVED  ,//date key  ,// = Дате формирования файла
DTSTADDR    //date key   // Дата внесения записи - Адрес
from EGR 
where INDIVID == 0 
and  XMLDATE > @dat1;

