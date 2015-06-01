CREATE OR REPLACE VIEW JKDA_PERSON_BASE_INFO_EXP AS
select a.*,nvl(b.ybzk_height,'') ybzk_height,nvl(b.ybzk_weight,'') ybzk_weight,nvl(b.ybzk_xy_l_high,'') ybzk_xy_l_high,nvl(b.ybzk_xy_l_low,'') ybzk_xy_l_low,nvl(b.ybzk_xy_r_high,'') ybzk_xy_r_high,nvl(b.ybzk_xy_r_low,'') ybzk_xy_r_low,nvl(c.fzjc_kfxt_l,'') fzjc_kfxt_l,nvl(c.fzjc_kfxt_dl,'') fzjc_kfxt_dl,nvl(c.fzjc_chxt,'') fzjc_chxt,nvl(c.fzjc_sjxt,'') fzjc_sjxt from
jkda_person_base_info a,
(select tt.* from (SELECT  t.* ,row_number() over(partition by b_id order by tj_date desc, xh_no desc) row_num
                    FROM jkda_person_tj_ybzk t ) tt where tt.row_num=1 )b,
(select tt.* from (SELECT  t.* ,row_number() over(partition by b_id order by tj_date desc, xh_no desc) row_num
                    FROM jkda_person_tj_fzjc t ) tt where tt.row_num=1 )c
where a.b_id=b.b_id(+) and a.b_id=c.b_id(+) 
					 

------
----慢病管理高血压的病情流程表（随访记录表）visit_date-------------
CREATE OR REPLACE VIEW JKDA_PERSON_MAIN_PROBLEM_GXY AS 
select a.*,nvl(b.ybzk_height,'') ybzk_height,nvl(b.ybzk_weight,'') ybzk_weight ,nvl(b.ybzk_ssy,'') ybzk_ssy,nvl(b.ybzk_szy,'') ybzk_szy 
from jkda_person_main_problem a,
(select tt.* from (SELECT  t.* ,row_number() over(partition by b_id order by visit_date desc) row_num
                    FROM jkda_slow_dis_bqlc_gxy t ) tt where tt.row_num=1 ) b ,
jkda_dic_health_problems c                    
where a.b_id=b.b_id(+)  and a.T_Id_No=c.ID_NO(+) and c.T_Type='01' and c.W_Id_No='M001'                 

jkda_slow_dis_bqlc_gxy

---------改造------------------------------
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
自定义档案号  jm_bh 

jkda_person_tj_ybzk

身高(CM) ybzk_height
体重(KG) ybzk_weight
血压 左侧收缩压（mmHg） ybzk_xy_l_high
血压 左侧舒张压（mmHg） ybzk_xy_l_low
血压 右侧收缩压（mmHg） ybzk_xy_r_high
血压 右侧舒张压（mmHg） ybzk_xy_r_low
症状 ybzk_zz

jkda_person_tj_shfs

日吸烟量      shfs_xyqk_day  '支/日'
饮酒量        shfs_yjqk_day  '两'
每次锻炼时间  shfs_tydl_each '分钟/每次'




----慢病管理糖尿病的病情流程表（随访记录表）visit_date----------------
CREATE OR REPLACE VIEW JKDA_PERSON_MAIN_PROBLEM_TNB AS 
select a.*,nvl(b.ybzk_height,'') ybzk_height,nvl(b.ybzk_weight,'') ybzk_weight ,nvl(b.kfxt_value,'') kfxt_value,nvl(b.khxt_value,'') khxt_value ,nvl(b.sjxt_value,'') sjxt_value 
from jkda_person_main_problem a,
(select tt.* from (SELECT  t.* ,row_number() over(partition by b_id order by visit_date desc) row_num
                    FROM jkda_slow_dis_bqlc_tnb t ) tt where tt.row_num=1 ) b ,
jkda_dic_health_problems c                    
where a.b_id=b.b_id(+)  and a.T_Id_No=c.ID_NO(+) and c.T_Type='01' and c.W_Id_No='M003'  

身高(CM) ybzk_height
体重(KG) ybzk_weight
空腹血糖值 kfxt_value
餐后血糖值 khxt_value
随机血糖值 sjxt_value

jkda_slow_dis_bqlc_tnb

---------------------------改造-------------------------------
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

空腹血糖(mmol/L) fzjc_kfxt_l
空腹血糖(mg/dL)  fzjc_kfxt_dl
餐后血糖值 fzjc_chxt
随机血糖值 fzjc_sjxt


