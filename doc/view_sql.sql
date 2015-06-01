CREATE OR REPLACE VIEW JKDA_PERSON_BASE_INFO_EXP AS
select a.*,nvl(b.ybzk_height,'') ybzk_height,nvl(b.ybzk_weight,'') ybzk_weight,nvl(b.ybzk_xy_l_high,'') ybzk_xy_l_high,nvl(b.ybzk_xy_l_low,'') ybzk_xy_l_low,nvl(b.ybzk_xy_r_high,'') ybzk_xy_r_high,nvl(b.ybzk_xy_r_low,'') ybzk_xy_r_low,nvl(c.fzjc_kfxt_l,'') fzjc_kfxt_l,nvl(c.fzjc_kfxt_dl,'') fzjc_kfxt_dl,nvl(c.fzjc_chxt,'') fzjc_chxt,nvl(c.fzjc_sjxt,'') fzjc_sjxt from
jkda_person_base_info a,
(select tt.* from (SELECT  t.* ,row_number() over(partition by b_id order by tj_date desc, xh_no desc) row_num
                    FROM jkda_person_tj_ybzk t ) tt where tt.row_num=1 )b,
(select tt.* from (SELECT  t.* ,row_number() over(partition by b_id order by tj_date desc, xh_no desc) row_num
                    FROM jkda_person_tj_fzjc t ) tt where tt.row_num=1 )c
where a.b_id=b.b_id(+) and a.b_id=c.b_id(+) 
					 

------
----���������Ѫѹ�Ĳ������̱���ü�¼��visit_date-------------
CREATE OR REPLACE VIEW JKDA_PERSON_MAIN_PROBLEM_GXY AS 
select a.*,nvl(b.ybzk_height,'') ybzk_height,nvl(b.ybzk_weight,'') ybzk_weight ,nvl(b.ybzk_ssy,'') ybzk_ssy,nvl(b.ybzk_szy,'') ybzk_szy 
from jkda_person_main_problem a,
(select tt.* from (SELECT  t.* ,row_number() over(partition by b_id order by visit_date desc) row_num
                    FROM jkda_slow_dis_bqlc_gxy t ) tt where tt.row_num=1 ) b ,
jkda_dic_health_problems c                    
where a.b_id=b.b_id(+)  and a.T_Id_No=c.ID_NO(+) and c.T_Type='01' and c.W_Id_No='M001'                 

jkda_slow_dis_bqlc_gxy

---------����------------------------------
CREATE OR REPLACE VIEW JKDA_PERSON_MAIN_PROBLEM_GXY AS 
select a.*,nvl(b.ybzk_height,'') ybzk_height,nvl(b.ybzk_weight,'') ybzk_weight ,
nvl(b.ybzk_xy_l_high,'') ybzk_xy_l_high,nvl(b.ybzk_xy_l_low,'') ybzk_xy_l_low,
nvl(b.ybzk_xy_r_high,'') ybzk_xy_r_high,nvl(b.ybzk_xy_r_low,'') ybzk_xy_r_low,nvl(b.ybzk_zz,'') ybzk_zz,
nvl(d.shfs_xyqk_day,'') shfs_xyqk_day,nvl(d.shfs_yjqk_day,'') shfs_yjqk_day,nvl(d.shfs_tydl_each,'') shfs_tydl_each 
from jkda_person_main_problem a,
(select tt.* from (SELECT  t.* ,row_number() over(partition by b_id order by tj_date desc) row_num
                    FROM jkda_person_tj_ybzk t ) tt where tt.row_num=1 ) b ,
jkda_dic_health_problems c,
(select tt.* from (SELECT  t.* ,row_number() over(partition by b_id order by tj_date desc) row_num
                    FROM jkda_person_tj_shfs t ) tt where tt.row_num=1 ) d
where a.b_id=b.b_id(+) and a.b_id=d.b_id(+)  and a.T_Id_No=c.ID_NO(+) and c.T_Type='01' and c.W_Id_No='M001' 

jkda_person_base_info
�Զ��嵵����  jm_bh 

jkda_person_tj_ybzk

���(CM) ybzk_height
����(KG) ybzk_weight
Ѫѹ �������ѹ��mmHg�� ybzk_xy_l_high
Ѫѹ �������ѹ��mmHg�� ybzk_xy_l_low
Ѫѹ �Ҳ�����ѹ��mmHg�� ybzk_xy_r_high
Ѫѹ �Ҳ�����ѹ��mmHg�� ybzk_xy_r_low
֢״ ybzk_zz

jkda_person_tj_shfs

��������      shfs_xyqk_day  '֧/��'
������        shfs_yjqk_day  '��'
ÿ�ζ���ʱ��  shfs_tydl_each '����/ÿ��'




----�����������򲡵Ĳ������̱���ü�¼��visit_date----------------
CREATE OR REPLACE VIEW JKDA_PERSON_MAIN_PROBLEM_TNB AS 
select a.*,nvl(b.ybzk_height,'') ybzk_height,nvl(b.ybzk_weight,'') ybzk_weight ,nvl(b.kfxt_value,'') kfxt_value,nvl(b.khxt_value,'') khxt_value ,nvl(b.sjxt_value,'') sjxt_value 
from jkda_person_main_problem a,
(select tt.* from (SELECT  t.* ,row_number() over(partition by b_id order by visit_date desc) row_num
                    FROM jkda_slow_dis_bqlc_tnb t ) tt where tt.row_num=1 ) b ,
jkda_dic_health_problems c                    
where a.b_id=b.b_id(+)  and a.T_Id_No=c.ID_NO(+) and c.T_Type='01' and c.W_Id_No='M003'  

���(CM) ybzk_height
����(KG) ybzk_weight
�ո�Ѫ��ֵ kfxt_value
�ͺ�Ѫ��ֵ khxt_value
���Ѫ��ֵ sjxt_value

jkda_slow_dis_bqlc_tnb

---------------------------����-------------------------------
CREATE OR REPLACE VIEW JKDA_PERSON_MAIN_PROBLEM_TNB AS 
select a.*,nvl(b.ybzk_height,'') ybzk_height,nvl(b.ybzk_weight,'') ybzk_weight ,
nvl(e.fzjc_kfxt_l,'') fzjc_kfxt_l,nvl(e.fzjc_kfxt_dl,'') fzjc_kfxt_dl ,nvl(e.fzjc_chxt,'') fzjc_chxt, nvl(e.fzjc_sjxt,'') fzjc_sjxt,
nvl(b.ybzk_zz,'') ybzk_zz,
nvl(d.shfs_xyqk_day,'') shfs_xyqk_day,nvl(d.shfs_yjqk_day,'') shfs_yjqk_day,nvl(d.shfs_tydl_each,'') shfs_tydl_each 
from jkda_person_main_problem a,
(select tt.* from (SELECT  t.* ,row_number() over(partition by b_id order by tj_date desc) row_num
                    FROM jkda_person_tj_ybzk t ) tt where tt.row_num=1 ) b ,
jkda_dic_health_problems c,
(select tt.* from (SELECT  t.* ,row_number() over(partition by b_id order by tj_date desc) row_num
                    FROM jkda_person_tj_shfs t ) tt where tt.row_num=1 ) d,
(select tt.* from (SELECT  t.* ,row_number() over(partition by b_id order by tj_date desc) row_num
                    FROM jkda_person_tj_fzjc t ) tt where tt.row_num=1 ) e                    
where a.b_id=b.b_id(+) and a.b_id=d.b_id(+) and a.b_id=e.b_id(+)  and a.T_Id_No=c.ID_NO(+) and c.T_Type='01' and c.W_Id_No='M003' 


jkda_person_tj_fzjc

�ո�Ѫ��(mmol/L) fzjc_kfxt_l
�ո�Ѫ��(mg/dL)  fzjc_kfxt_dl
�ͺ�Ѫ��ֵ fzjc_chxt
���Ѫ��ֵ fzjc_sjxt


