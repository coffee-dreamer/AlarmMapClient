-----------------序列
create sequence SEQ_OPRLOGID
minvalue 1
maxvalue 999999999999999999999999999
start with 1
increment by 1;



--居民基本信息表
create table jkda_person_base_info 
(
   b_id                 varchar(30)                    not null,
   home_lsh             varchar(30)                    not null,
   jm_bh                varchar(30)                    ,
   mz_patient_no        varchar(12)                    ,
   person_name          varchar(32)                    ,
   father_name          varchar(32)                    ,
   mother_name          varchar(32)                    ,
   p_sex                int                            ,
   p_age                int                            ,
   p_birth              date                       not null,
   p_card_no            varchar(20)                    ,
   p_rygx               varchar(10)                    ,
   p_bjht               varchar(1)                     default '0',
   work_unit            varchar(50)                    ,
   p_tel                varchar(15)                    ,
   link_person          varchar(20)                    ,
   link_tel             varchar(15)                    ,
   cz_type              varchar(20)                    ,
   mz_type              varchar(30)                    ,
   address_sf           varchar(30)                    not null,
   address_ds           varchar(30)                    not null,
   address_qx           varchar(30)                    not null,
   address_jd           varchar(30)                    not null,
   address_jwh          varchar(30)                    not null,
   address_group        varchar(30)                    ,
   address_qt           varchar(50)                    ,
   xx_flag              varchar(20)                    ,
   xx_type              varchar(10)                    ,
   whcd_type            varchar(20)                    ,
   professional         varchar(100)                   ,
   marital_status       varchar(20)                    ,
   expence_type         varchar(20)                    ,
   qt_expence           varchar(100)                   ,
   drug_allergy         varchar(20)                    ,
   allergy_memo         varchar(100)                   ,
   jzs_father           varchar(50)                    ,
   jzs_father_memo      varchar(100)                   ,
   jzs_mother           varchar(50)                    ,
   jzs_mother_memo      varchar(100)                   ,
   jzs_siblings         varchar(50)                    ,
   jzs_siblings_memo    varchar(100)                   ,
   jzs_children         varchar(50)                    ,
   jzs_children_memo    varchar(100)                   ,
   ycs_flag             varchar(1)                     ,
   ycs_memo             varchar(200)                   ,
   cjqk_flag            varchar(50)                    ,
   cjqk_memo            varchar(100)                   ,
   jws_memo             varchar(300)                   ,
   social_card          varchar(32)                    ,
   vacate_flag          char(1)                        default '0',
   vacate_date          date                       ,
   death_flag           char(1)                        default '0',
   death_date           date                       ,
   p_photo              varchar(300)                          ,
   investigator         varchar(32)                    not null,
   invest_date          date                       default sysdate,
   input_op             varchar(32)                    not null,
   input_date           date                       default sysdate,
   update_op            varchar(32)                    ,
   update_date          date                       ,
   yjd_flag             varchar(1)                     default '1',
   hj_address           varchar(100)                   ,
   jd_hosp              varchar(50)                    ,
   jd_op                varchar(32)                    ,
   jd_date              date                       ,
   death_reason         varchar(100)                   ,
   hosp_id              varchar(6)                     ,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                       ,
   primary key (b_id)
);

create table jkda_person_main_problem 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   fs_date              date                           ,
   dj_date              date                       ,
   t_type               varchar(2)                     ,
   t_id_no              varchar(10)                    ,
   t_name               varchar(100)                   ,
   t_memo               varchar(1000)                  ,
   jj_date              date                       ,
   input_op             varchar(20)                    ,
   input_date           date                       ,
   begin_when           varchar(20)                    ,
   disease_hbz          varchar(200)                   ,
   disease_related      varchar(100)                   ,
   used_drugs           varchar(500)                   ,
   cur_situation        varchar(20)                    ,
   class_type           varchar(100)                   ,
   treat_method         varchar(100)                   ,
   class_desc           varchar(200)                   ,
   problem_memo         varchar(200)                   ,
   manage_date          date                       ,
   rej_manage_flag      varchar(1)                      default '2',
   end_manage_date      date                       ,
   manage_result        varchar(1)                     ,
   cj_card_no           varchar(20)                    ,
   dis_modify_op        varchar(20)                    ,
   dis_modify_date      date                       ,
   zy_flag              varchar(1)                      default '0',
   update_op            varchar(32)                    ,
   update_date          date                       ,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                       ,
   hosp_id              varchar(6)                     not null,
   primary key (b_id, xh_no)
);

create table jkda_person_heath_education 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   xz_rq                date                       not null,
   xz_dor               varchar(32)                    not null,
   xz_memo              clob                           ,
   zx_memo              clob                           ,
   zz_memo              clob                           ,
   input_op             varchar(20)                    ,
   input_date           date                       ,
   update_op            varchar(32)                    ,
   update_date          date                       ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                       ,
   primary key (b_id, xh_no)
);

create table jkda_person_temp_problem 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   fs_date              date                       ,
   zr_dor               varchar(32)                    ,
   t_id_no              varchar(10)                    ,
   t_name               varchar(100)                   ,
   jj_date              date                       ,
   zz_tz                clob                           ,
   zlzd_opinion         clob                           ,
   input_op             varchar(20)                    ,
   input_date           date                       ,
   modify_op            varchar(20)                    ,
   modify_date          date                       ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                           default 1,
   check_op             varchar(6)                     ,
   check_date           date                       ,
   primary key  (b_id, xh_no)
);

--居民周期性检查 - 一般状况
create table jkda_person_tj_ybzk 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   tj_date              date                           ,
   zr_dor               varchar(20)                    ,
   ybzk_zz              varchar(300)                   ,
   ybzk_tw              numeric(9,1)                   ,
   ybzk_ml              int                            ,
   ybzk_hxpl            int                            ,
   ybzk_xy_l_high       int                            ,
   ybzk_xy_l_low        int                            ,
   ybzk_xy_r_high       int                            ,
   ybzk_xy_r_low        int                            ,
   ybzk_height          numeric(9,1)                   ,
   ybzk_weight          numeric(9,1)                   ,
   ybzk_waist           numeric(9,1)                   ,
   ybzk_tzzs            numeric(9,2)                   ,
   ybzk_hip             numeric(9,1)                   ,
   ybzk_ytwbz           numeric(9,2)                   ,
   ybzk_old_cog         varchar(1)                     ,
   ybzk_cog_score       numeric(9,2)                   ,
   ybzk_old_emo         varchar(1)                     ,
   ybzk_emo_score       numeric(9,2)                   ,
   input_op             varchar(20)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     ,
   check_flag           int                            ,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, xh_no)
);

create table jkda_person_tj_zytz 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   tj_date              date                           ,
   zr_dor               varchar(20)                    ,
   zytz_phz_flag        varchar(1)                     ,
   zytz_qxz_flag        varchar(1)                     ,
   zytz_yaxz_flag       varchar(1)                     ,
   zytz_yixz_flag       varchar(1)                     ,
   zytz_tsz_flag        varchar(1)                     ,
   zytz_srz_flag        varchar(1)                     ,
   zytz_xyz_flag        varchar(1)                     ,
   zytz_qyz_flag        varchar(1)                     ,
   zytz_tbz_flag        varchar(1)                     ,
   input_op             varchar(20)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                             default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, xh_no)
);

create table jkda_person_tj_tjjl 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   tj_date              date                           ,
   zr_dor               varchar(20)                    ,
   jkpj_flag            varchar(1)                     ,
   jkpj_yc              varchar(1000)                  ,
   jkzd_flag            varchar(20)                    ,
   wxyskz_flag          varchar(20)                    ,
   wxyskz_jtz_memo      varchar(50)                    ,
   wxyskz_ymjz_memo     varchar(50)                    ,
   wxyskz_qt_memo       varchar(50)                    ,
   input_op             varchar(20)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   next_sf_date         date                           ,
   primary key (b_id, xh_no)
);

create table jkda_person_tj_ct 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   tj_date              date                           ,
   zr_dor               varchar(20)                    ,
   ct_skin              varchar(20)                    ,
   ct_skin_memo         varchar(50)                    ,
   ct_sclera            varchar(20)                    ,
   ct_sclera_memo       varchar(50)                    ,
   ct_lymph             varchar(20)                    ,
   ct_lymph_memo        varchar(50)                    ,
   ct_lung_tzx          varchar(1)                     ,
   ct_lung_hxy          varchar(1)                     ,
   ct_lung_hxy_memo     varchar(50)                    ,
   ct_lung_ly           varchar(20)                    ,
   ct_lung_ly_memo      varchar(50)                    ,
   ct_heart_freq        int                            ,
   ct_heart_rhythm      varchar(1)                     ,
   ct_heart_zy          varchar(1)                     ,
   ct_heart_zy_memo     varchar(50)                    ,
   ct_belly_yt          varchar(1)                     ,
   ct_belly_yt_memo     varchar(50)                    ,
   ct_belly_bk          varchar(1)                     ,
   ct_belly_bk_memo     varchar(50)                    ,
   ct_belly_gd          varchar(1)                     ,
   ct_belly_gd_memo     varchar(50)                    ,
   ct_belly_pd          varchar(1)                     ,
   ct_belly_pd_memo     varchar(50)                    ,
   ct_belly_zy          varchar(1)                     ,
   ct_belly_zy_memo     varchar(50)                    ,
   ct_xzsz_flag         varchar(1)                     ,
   ct_zbdmbd_flag       varchar(1)                     ,
   ct_gmzz_flag         varchar(1)                     ,
   ct_gmzz_memo         varchar(50)                    ,
   ct_breast_flag       varchar(20)                    ,
   ct_breast_memo       varchar(50)                    ,
   ct_fk_wy_flag        varchar(1)                     ,
   ct_fk_wy_memo        varchar(50)                    ,
   ct_fk_yd_flag        varchar(1)                     ,
   ct_fk_yd_memo        varchar(50)                    ,
   ct_fk_gj_flag        varchar(1)                     ,
   ct_fk_gj_memo        varchar(50)                    ,
   ct_fk_gt_flag        varchar(1)                     ,
   ct_fk_gt_memo        varchar(50)                    ,
   ct_fk_fj_flag        varchar(1)                     ,
   ct_fk_fj_memo        varchar(50)                    ,
   ct_qt_memo           varchar(100)                   ,
   input_op             varchar(20)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, xh_no)
);

create table jkda_person_tj_shfs 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   tj_date              date                           ,
   zr_dor               varchar(20)                    ,
   shfs_tydl_freq       varchar(30)                    ,
   shfs_tydl_each       int                            ,
   shfs_tydl_hold       numeric(9,2)                   ,
   shfs_tydl_way        varchar(100)                   ,
   shfs_ysxg            varchar(20)                    ,
   shfs_xyqk_freq       varchar(30)                    ,
   shfs_xyqk_day        numeric(9,2)                   ,
   shfs_xyqk_begin      int                            ,
   shfs_xyqk_end        int                            ,
   shfs_yjqk_freq       varchar(30)                    ,
   shfs_yjqk_day        numeric(9,2)                   ,
   shfs_yjqk_jj_flag    varchar(30)                    ,
   shfs_yjqk_jj_age     int                            ,
   shfs_yjqk_begin      int                            ,
   shfs_yjqk_zj_flag    varchar(1)                     ,
   shfs_yjqk_type       varchar(20)                    ,
   shfs_yjqk_qt_memo    varchar(50)                    ,
   shfs_ywyl            varchar(20)                    ,
   shfs_ywyl_memo       varchar(50)                    ,
   shfs_work_flag       varchar(1)                     ,
   shfs_work_memo       varchar(50)                    ,
   shfs_work_year       numeric(9,2)                   ,
   shfs_work_type_hxp   varchar(50)                    ,
   shfs_work_type_hxp_fh varchar(1)                    default '1',
   shfs_work_type_hxp_fhcs varchar(50)                 ,
   shfs_work_type_dw    varchar(50)                    ,
   shfs_work_type_dw_fh varchar(1)                      default '1',
   shfs_work_type_dw_fhcs varchar(50)                    ,
   shfs_work_type_sx    varchar(50)                    ,
   shfs_work_type_sx_fh varchar(1)                      default '1',
   shfs_work_type_sx_fhcs varchar(50)                    ,
   input_op             varchar(32)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, xh_no)
);

create table jkda_person_tj_zqgn 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   tj_date              date                           ,
   zr_dor               varchar(20)                    ,
   zqgn_kq_kc           varchar(20)                    ,
   zqgn_kq_kc_memo      varchar(30)                    ,
   zqgn_kq_cl           varchar(20)                    ,
   zqgn_kq_qc_memo      varchar(50)                    ,
   zqgn_kq_yc_memo      varchar(50)                    ,
   zqgn_kq_jy_memo      varchar(50)                    ,
   zqgn_kq_yb           varchar(20)                    ,
   zqgn_kq_yb_memo      varchar(50)                    ,
   zqgn_sl_l            numeric(9,2)                   ,
   zqgn_sl_l_jz         numeric(9,2)                   ,
   zqgn_sl_r            numeric(9,2)                   ,
   zqgn_sl_r_jz         numeric(9,2)                   ,
   zqgn_tl_flag         varchar(1)                     ,
   zqgn_ydgn            varchar(1)                     ,
   input_op             varchar(20)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   zqgn_kq_qc_memo_1    varchar(50)                    ,
   zqgn_kq_qc_memo_2    varchar(50)                    ,
   zqgn_kq_qc_memo_3    varchar(50)                    ,
   zqgn_kq_yc_memo_1    varchar(50)                    ,
   zqgn_kq_yc_memo_2    varchar(50)                    ,
   zqgn_kq_yc_memo_3    varchar(50)                    ,
   zqgn_kq_jy_memo_1    varchar(50)                    ,
   zqgn_kq_jy_memo_2    varchar(50)                    ,
   zqgn_kq_jy_memo_3    varchar(50)                    ,
   zqgn_sl_l_memo       varchar(50)                    ,
   zqgn_sl_l_jz_ds      int                            ,
   zqgn_sl_r_memo       varchar(50)                    ,
   zqgn_sl_r_jz_ds      int                            ,
   primary key (b_id, xh_no)
);

create table jkda_person_tj_fzjc 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   tj_date              date                           ,
   zr_dor               varchar(20)                    ,
   fzjc_kfxt_l          numeric(9,2)                   ,
   fzjc_kfxt_dl         numeric(9,2)                   ,
   fzjc_kfxt_memo       varchar(20)                    ,
   fzjc_chxt            numeric(18,2)                  ,
   fzjc_chxt_memo       varchar(20)                    ,
   fzjc_xcg_xhdb        numeric(18,2)                        ,
   fzjc_xcg_xhdb_memo   varchar(20)                    ,
   fzjc_xcg_bxb         numeric(18,2)                        ,
   fzjc_xcg_bxb_memo    varchar(20)                    ,
   fzjc_xcg_xxb         numeric(18,2)                        ,
   fzjc_xcg_xxb_memo    varchar(20)                    ,
   fzjc_xcg_qt          varchar(100)                   ,
   fzjc_ncg_ndb         varchar(30)                    ,
   fzjc_ncg_nt          varchar(30)                    ,
   fzjc_ncg_ntt         varchar(30)                    ,
   fzjc_ncg_ntx         varchar(30)                    ,
   fzjc_ncg_qt          varchar(100)                   ,
   fzjc_nwlbdb          numeric(18,2)                        ,
   fzjc_nwlbdb_memo     varchar(20)                    ,
   fzjc_dbqx            varchar(1)                     ,
   fzjc_ggn_bzam        numeric(9,1)                   ,
   fzjc_ggn_bzam_memo   varchar(20)                    ,
   fzjc_ggn_czam        numeric(9,1)                   ,
   fzjc_ggn_czam_memo   varchar(20)                    ,
   fzjc_ggn_bdb         numeric(9,1)                   ,
   fzjc_ggn_bdb_memo    varchar(20)                    ,
   fzjc_ggn_zdhs        numeric(9,1)                   ,
   fzjc_ggn_zdhs_memo   varchar(20)                    ,
   fzjc_ggn_jhdhs       numeric(9,1)                   ,
   fzjc_ggn_jhdhs_memo  varchar(20)                    ,
   fzjc_sgn_xqjg        numeric(9,1)                   ,
   fzjc_sgn_xqjg_memo   varchar(20)                    ,
   fzjc_sgn_xnsd        numeric(9,2)                   ,
   fzjc_sgn_xnsd_memo   varchar(20)                    ,
   fzjc_sgn_xjnd        numeric(9,1)                   ,
   fzjc_sgn_xjnd_memo   varchar(20)                    ,
   fzjc_sgn_xnnd        numeric(9,1)                   ,
   fzjc_sgn_xnnd_memo   varchar(20)                    ,
   fzjc_xz_zdgc         numeric(9,2)                   ,
   fzjc_xz_zdgc_memo    varchar(20)                    ,
   fzjc_xz_gysz         numeric(9,2)                   ,
   fzjc_xz_gysz_memo    varchar(20)                    ,
   fzjc_xz_dmd          numeric(9,2)                   ,
   fzjc_xz_dmd_memo     varchar(20)                    ,
   fzjc_xz_gmd          numeric(9,2)                   ,
   fzjc_xz_gmd_memo     varchar(20)                    ,
   fzjc_thxhdb          numeric(9,2)                   ,
   fzjc_thxhdb_memo     varchar(20)                    ,
   fzjc_ygbmky          varchar(1)                     ,
   fzjc_yd_flag         varchar(1)                     ,
   fzjc_yd_memo         varchar(200)                   ,
   fzjc_xdt_flag        varchar(1)                     ,
   fzjc_xdt_memo        varchar(200)                   ,
   fzjc_xbxx_flag       varchar(1)                     ,
   fzjc_xbxx_memo       varchar(200)                   ,
   fzjc_bc_flag         varchar(1)                     ,
   fzjc_bc_memo         varchar(200)                   ,
   fzjc_gjtp_flag       varchar(1)                     ,
   fzjc_gjtp_memo       varchar(200)                   ,
   fzjc_qt_memo         varchar(300)                   ,
   input_op             varchar(20)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, xh_no)
);

--主要健康问题字典表
create table jkda_dic_health_problems 
(
   id_no                varchar(10)                    not null,
   t_type               varchar(2)                     not null,
    T_TYPE_NAME         VARCHAR2(100),
   t_name               varchar(100)                   ,
   py_code              varchar(50)                    ,
   qt_code              varchar(50)                    ,
   w_id_no              varchar(6)                     ,
   icd_code             varchar(10)                    ,
   icd_id               numeric                        ,
   jb_type              varchar(10)                    ,
   primary key (id_no)
);

--慢病管理综合病种的病情流程表（随访记录表）
create table jkda_slow_dis_bqlc_zhbz 
(
   b_id                 varchar(30)                    not null,
   pro_xh_no            int                            not null,
   bqlc_xh_no           int                            not null,
   visit_date           date                           not null,
   visit_dor_name       varchar(20)                    ,
   ybzk_zz              varchar(50)                    ,
   ybzk_height          numeric(18,2)                  ,
   ybzk_weight          numeric(18,2)                  ,
   ybzk_ssy             int                            ,
   ybzk_szy             int                            ,
   ybzk_tw              numeric(18,2)                  ,
   ybzk_mb              int                            ,
   ybzk_xl              int                            ,
   xt                   varchar(100)                   ,
   zlcs                 clob                           ,
   bjzd                 clob                           ,
   input_op             varchar(10)                    not null,
   input_date           date                           not null,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   next_sf_date         date                           ,
   primary key (b_id, pro_xh_no, bqlc_xh_no)
);



---慢病管理SOAP记录表
create table jkda_slow_dis_soap 
(
   b_id                 varchar(30)                    not null,
   pro_xh_no            int                            not null,
   soap_xh_no           int                            not null,
   jc_date              date                           not null,
   s_zs_memo            clob                           ,
   o_bs_memo            clob                           ,
   a_zd_memo            clob                           ,
   p_bj_memo            clob                           ,
   input_op             varchar(20)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, pro_xh_no, soap_xh_no)
);

--慢病管理高血压的病情流程表（随访记录表）
create table jkda_slow_dis_bqlc_gxy 
(
   b_id                 varchar(30)                    not null,
   pro_xh_no            int                            not null,
   bqlc_xh_no           int                            not null,
   visit_date           date                           not null,
   visit_dor_name       varchar(20)                    ,
   ybzk_zz              varchar(200)                   ,
   ybzk_height          numeric(18,2)                  ,
   ybzk_weight          numeric(18,2)                  ,
   ybzk_tzzs            numeric(18,2)                  ,
   ybzk_ssy             int                            ,
   ybzk_szy             int                            ,
   xtz                  varchar(100)                   ,
   yd                   varchar(30)                    ,
   xdt                  varchar(100)                   ,
   ncg                  varchar(100)                   ,
   dgc                  numeric(18,2)                  ,
   gysz                 numeric(18,2)                  ,
   dmdzdb               numeric(18,2)                  ,
   gmdzdb               numeric(18,2)                  ,
   hy_qt                varchar(50)                    ,
   other                varchar(100)                   ,
   zlcs                 clob                           ,
   bjzd                 clob                           ,
   input_op             varchar(10)                    not null,
   input_date           date                           not null,
   sffs_type            varchar(20)                    ,
   weight_next          numeric(18,2)                  ,
   heart_rate           int                            ,
   heart_rate_next      int                            ,
   day_smoking          numeric(9,2)                   ,
   day_smoking_next     numeric(9,2)                   ,
   day_drinking         numeric(9,2)                   ,
   day_drinking_next    numeric(9,2)                   ,
   week_sport           numeric(9,2)                   ,
   week_sport_next      numeric(9,2)                   ,
   sport_long           numeric(9,2)                   ,
   sport_long_next      numeric(9,2)                   ,
   day_salt             numeric(9,2)                   ,
   day_salt_next        numeric(9,2)                   ,
   xltz_type            varchar(20)                    ,
   zyxw_type            varchar(20)                    ,
   fzjc_memo            varchar(1000)                  ,
   fyycx_flag           varchar(20)                    ,
   drug_blfy_flag       varchar(1)                     ,
   drug_blfy_memo       varchar(200)                   ,
   this_sf_effect       varchar(50)                    ,
   eat_drugname_1       varchar(50)                    ,
   eat_drugtype_1       varchar(10)                    ,
   eat_drugdosage_1     varchar(20)                    ,
   eat_drugname_2       varchar(50)                    ,
   eat_drugtype_2       varchar(10)                    ,
   eat_drugdosage_2     varchar(20)                    ,
   eat_drugname_3       varchar(50)                    ,
   eat_drugtype_3       varchar(10)                    ,
   eat_drugdosage_3     varchar(20)                    ,
   eat_drugname_qt      varchar(200)                   ,
   zz_reason            varchar(100)                   ,
   zz_hosp_and_dept     varchar(100)                   ,
   next_sf_date         date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, pro_xh_no, bqlc_xh_no)
);

--慢病管理高血压的防治效果评价
create table jkda_slow_dis_crbrs_gxy 
(
   b_id                 varchar(30)                    not null,
   pro_xh_no            int                            not null,
   crbrs_xh_no          int                            not null,
   pj_date              date                           ,
   bfz_memo             varchar(200)                   ,
   fb_date              date                           ,
   xtjc_flag            varchar(1)                     default '0',
   eat_jyy              varchar(300)                   ,
   pg_result            varchar(20)                    default '未知',
   input_op             varchar(6)                     ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, pro_xh_no, crbrs_xh_no)
);

--慢病管理脑血管的病情流程表（随访记录表）
create table jkda_slow_dis_bqlc_nxg 
(
   b_id                 varchar(30)                    not null,
   pro_xh_no            int                            not null,
   nxgbq_xh_no          int                            not null,
   visit_date           date                           not null,
   visit_dor_name       varchar(20)                    ,
   ybzk_zz              varchar(200)                   ,
   ybzk_ssy             int                            ,
   ybzk_szy             int                            ,
   jszt                 varchar(50)                    ,
   zlnl                 varchar(50)                    ,
   heart                varchar(100)                   ,
   lung                 varchar(100)                   ,
   sjjc                 varchar(100)                   ,
   jl                   varchar(100)                   ,
   xdt                  varchar(100)                   ,
   other                varchar(100)                   ,
   xt                   numeric(18,2)                  ,
   dgc                  numeric(18,2)                  ,
   gysz                 numeric(18,2)                  ,
   dmddb                numeric(18,2)                  ,
   gmddb                numeric(18,2)                  ,
   hy_other             varchar(100)                   ,
   zlcs                 varchar(1000)                  ,
   bjzd                 varchar(1000)                  ,
   input_op             varchar(10)                    not null,
   input_date           date                           not null,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   next_sf_date         date                           ,
   primary key (b_id, pro_xh_no, nxgbq_xh_no)
);

---慢病管理脑血管的CRBRS行为量表
create table jkda_slow_dis_crbrs_nxg 
(
   b_id                 varchar(30)                    not null,
   pro_xh_no            int                            not null,
   crbr_xh_no           int                            not null,
   pj_date              date                           ,
   qtzg1                int                            default 0,
   qtzg2                int                             default 0,
   qtzg3                int                             default 0,
   qtzg4                int                             default 0,
   qtzg5                int                             default 0,
   qtzg_sum             int                             default 0,
   jsza1                int                             default 0,
   jsza2                int                             default 0,
   jsza3                int                             default 0,
   jsza4                int                             default 0,
   jsza5                int                             default 0,
   jsza_sum             int                             default 0,
   qtjb                 varchar(50)                    ,
   jsjb                 varchar(50)                    ,
   jshqtjb              varchar(50)                    ,
   input_op             varchar(10)                    not null,
   input_date           date                           not null,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, pro_xh_no, crbr_xh_no)
);

--慢病管理糖尿病的病情流程表（随访记录表）
create table jkda_slow_dis_bqlc_tnb 
(
   b_id                 varchar(30)                    not null,
   pro_xh_no            int                            not null,
   bqlc_xh_no           int                            not null,
   visit_date           date                           not null,
   visit_dor_name       varchar(20)                    ,
   ybzk_zz              varchar(200)                   ,
   ybzk_height          numeric(18,2)                  ,
   ybzk_weight          numeric(18,2)                  ,
   ybzk_tzzs            numeric(18,2)                  ,
   ybzk_ssy             int                            ,
   ybzk_szy             int                            ,
   xtz                  varchar(100)                   ,
   xdt                  varchar(100)                   ,
   other                varchar(100)                   ,
   tnb_check_flag       varchar(1)                     ,
   tb_memo              varchar(50)                    ,
   kfxt_check_flag      varchar(1)                     ,
   kfxt_value           numeric(18,2)                  ,
   chxt_check_flag      varchar(1)                     ,
   khxt_value           numeric(18,2)                  ,
   sjxt_check_flag      varchar(1)                     ,
   sjxt_value           numeric(18,2)                  ,
   thxhdb_check_flag    varchar(1)                     ,
   thxhdb_value         numeric(18,2)                  ,
   hy_qt                varchar(200)                   ,
   input_op             varchar(10)                    not null,
   input_date           date                           not null,
   sffs_type            varchar(20)                    ,
   weight_next          numeric(18,2)                  ,
   zbdmbd_flag          varchar(1)                     ,
   day_smoking          numeric(9,2)                   ,
   day_smoking_next     numeric(9,2)                   ,
   day_drinking         numeric(9,2)                   ,
   day_drinking_next    numeric(9,2)                   ,
   week_sport           numeric(9,2)                   ,
   week_sport_next      numeric(9,2)                   ,
   sport_long           numeric(9,2)                   ,
   sport_long_next      numeric(9,2)                   ,
   day_eat              numeric(9,2)                   ,
   day_eat_next         numeric(9,2)                   ,
   xltz_type            varchar(20)                    ,
   zyxw_type            varchar(20)                    ,
   thxhdb_check_date    date                           ,
   fzjc_memo            varchar(1000)                  ,
   fyycx_flag           varchar(20)                    ,
   drug_blfy_flag       varchar(1)                     ,
   drug_blfy_memo       varchar(200)                   ,
   dxtfy_flag           varchar(20)                    ,
   this_sf_effect       varchar(50)                    ,
   eat_drugname_1       varchar(50)                    ,
   eat_drugtype_1       varchar(10)                    ,
   eat_drugdosage_1     varchar(20)                    ,
   eat_drugname_2       varchar(50)                    ,
   eat_drugtype_2       varchar(10)                    ,
   eat_drugdosage_2     varchar(20)                    ,
   eat_drugname_3       varchar(50)                    ,
   eat_drugtype_3       varchar(10)                    ,
   eat_drugdosage_3     varchar(20)                    ,
   eat_drugname_yds     varchar(200)                   ,
   zz_reason            varchar(100)                   ,
   zz_hosp_and_dept     varchar(100)                   ,
   next_sf_date         date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, pro_xh_no, bqlc_xh_no)
);

--慢病管理糖尿病的治疗明细
create table jkda_slow_dis_cure_detail_tnb 
(
   b_id                 varchar(30)                    not null,
   pro_xh_no            int                            not null,
   cure_xh_no           int                            not null,
   cure_date            date                           not null,
   zr_dor               varchar(20)                    ,
   jkjy_way             varchar(50)                    ,
   jkjy_qt              varchar(100)                   ,
   yskz_qk              varchar(50)                    ,
   ydfs_qk              varchar(50)                    ,
   ydfs_qt              varchar(100)                   ,
   ydts_week            numeric(18,2)                  ,
   ydsj_day             numeric(18,2)                  ,
   yyqk                 clob                           ,
   jkzd                 clob                           ,
   input_op             varchar(10)                    not null,
   input_date           date                           not null,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, pro_xh_no, cure_xh_no)
);

--慢病管理糖尿病治疗效果评分
create table jkda_slow_dis_cure_effect_tnb
(
   b_id                 varchar(30)                    not null,
   pro_xh_no            int                            not null,
   grade_xh_no          int                            not null,
   grade_date           date                           not null,
   zr_dor               varchar(20)                    ,
   xykz_qk              varchar(50)                    ,
   xzkz_qk              varchar(50)                    ,
   xtkz_qk              varchar(50)                    ,
   tnb_bfz              varchar(50)                    ,
   grade_qk             varchar(50)                    ,
   input_op             varchar(10)                    not null,
   input_date           date                           not null,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, pro_xh_no, grade_xh_no)
);

--慢病管理冠心病的病情流程表（随访服务表）
create table jkda_slow_dis_bqlc_gxb 
(
   b_id                 varchar(30)                    not null,
   pro_xh_no            int                            not null,
   bqlc_xh_no           int                            not null,
   visit_date           date                           not null,
   visit_dor_name       varchar(20)                    ,
   ybzk_zz              varchar(50)                    ,
   ybzk_height          numeric(18,2)                  ,
   ybzk_weight          numeric(18,2)                  ,
   ybzk_tzzs            numeric(18,2)                  ,
   ybzk_ssy             int                            ,
   ybzk_szy             int                            ,
   heart                varchar(30)                    ,
   lung                 varchar(30)                    ,
   xdt                  varchar(100)                   ,
   ncg                  varchar(100)                   ,
   dgc                  numeric(18,2)                  ,
   gysz                 numeric(18,2)                  ,
   dmdzdb               numeric(18,2)                  ,
   gmdzdb               numeric(18,2)                  ,
   hy_qt                varchar(50)                    ,
   other                varchar(100)                   ,
   zlcs                 clob                           ,
   bjzd                 clob                           ,
   input_op             varchar(10)                    not null,
   input_date           date                           not null,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   next_sf_date         date                           ,
   primary key (b_id, pro_xh_no, bqlc_xh_no)
);

--慢病管理恶性肿瘤的病情流程表（随访服务表）
create table jkda_slow_dis_bqlc_exzl 
(
   b_id                 varchar(30)                    not null,
   pro_xh_no            int                            not null,
   exzl_xh_no           int                            not null,
   visit_date           date                           not null,
   visit_dor_name       varchar(20)                    ,
   ybzk_zz              varchar(50)                    ,
   ybzk_weight          numeric(18,2)                  ,
   ybzk_ssy             int                            ,
   ybzk_szy             int                            ,
   lbj                  varchar(100)                   ,
   heart                varchar(100)                   ,
   lung                 varchar(100)                   ,
   liver                varchar(100)                   ,
   spleen               varchar(100)                   ,
   yxjc                 varchar(100)                   ,
   zljbjc               varchar(100)                   ,
   xrt                  varchar(100)                   ,
   nrt                  varchar(100)                   ,
   stool                varchar(100)                   ,
   xc                   varchar(100)                   ,
   zlhy                 varchar(100)                   ,
   bc                   varchar(100)                   ,
   ct                   varchar(100)                   ,
   xsxts                varchar(100)                   ,
   other                varchar(200)                   ,
   zlcs                 varchar(1000)                  ,
   bjzd                 varchar(1000)                  ,
   input_op             varchar(10)                    not null,
   input_date           date                           not null,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   next_sf_date         date                           ,
   primary key  (b_id, pro_xh_no, exzl_xh_no)
);

---慢病管理慢阻肺病的病情流程表（随访记录表）
create table jkda_slow_dis_bqlc_mzfb 
(
   b_id                 varchar(30)                    not null,
   pro_xh_no            int                            not null,
   bqlc_xh_no           int                            not null,
   visit_date           date                           not null,
   visit_dor_name       varchar(20)                    ,
   zz_ks                varchar(50)                    ,
   zz_lt                varchar(50)                    ,
   zz_qc                varchar(50)                    ,
   zz_qt                varchar(50)                    ,
   ybzk_ssy             int                            ,
   ybzk_szy             int                            ,
   heart                varchar(50)                    ,
   lung                 varchar(50)                    ,
   lung_func            varchar(50)                    ,
   image_check          varchar(50)                    ,
   xdt                  varchar(100)                   ,
   ncg                  varchar(100)                   ,
   dgc                  numeric(18,2)                  ,
   gysz                 numeric(18,2)                  ,
   dmdzdb               numeric(18,2)                  ,
   gmdzdb               numeric(18,2)                  ,
   hy_qt                varchar(100)                   ,
   zlcs                 clob                           ,
   bjzd                 clob                           ,
   other                varchar(100)                   ,
   input_op             varchar(10)                    not null,
   input_date           date                           not null,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   next_sf_date         date                           ,
   primary key (b_id, pro_xh_no, bqlc_xh_no)
);

--残疾患者个人信息
create table jkda_cjgl_add_person_info 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   jhr_name             varchar(32)                    ,
   jhr_rygx             varchar(10)                    ,
   jhr_address          varchar(50)                    ,
   jhr_tel              varchar(15)                    ,
   jwh_link_tel         varchar(50)                    ,
   first_fb_date        date                           ,
   old_dis_kind         varchar(50)                    ,
   old_dis_qt           varchar(30)                    ,
   old_zl_mz            varchar(20)                    ,
   old_zl_zy            int                            ,
   last_zd_dis          varchar(50)                    ,
   last_zd_hosp         varchar(50)                    ,
   last_zd_date         date                           ,
   last_zl_kind         varchar(20)                    ,
   dis_eff_home         varchar(30)                    ,
   dis_eff_home_1       int                            ,
   dis_eff_home_2       int                            ,
   dis_eff_home_3       int                            ,
   dis_eff_home_4       int                            ,
   dis_eff_home_5       int                            ,
   lock_kind            varchar(20)                    ,
   add_date             date                           ,
   sign_dor             varchar(20)                    ,
   input_op             varchar(32)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   primary key (b_id, xh_no)
);

--残疾患者随访记录
create table jkda_cjgl_person_sfjl 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   sf_xh_no             int                            not null,
   visit_date           date                           ,
   visit_dor_name       varchar(20)                    ,
   now_dis              varchar(200)                   ,
   understand_kind      varchar(20)                    ,
   sleep_kind           varchar(10)                    ,
   eat_kind             varchar(10)                    ,
   shgn_kind_grshll     varchar(10)                    ,
   shgn_kind_jwld       varchar(10)                    ,
   shgn_kind_scld       varchar(20)                    ,
   shgn_kind_xxnl       varchar(10)                    ,
   shgn_kind_shrjjw     varchar(10)                    ,
   dis_eff_home         varchar(30)                    ,
   dis_eff_home_1       int                            ,
   dis_eff_home_2       int                            ,
   dis_eff_home_3       int                            ,
   dis_eff_home_4       int                            ,
   dis_eff_home_5       int                            ,
   lab_check            varchar(200)                   ,
   eat_drug             varchar(10)                    ,
   drug_bad_type        varchar(10)                    ,
   drug_bad_memo        varchar(200)                   ,
   zl_kind              varchar(10)                    ,
   now_sf_kind          varchar(10)                    ,
   zz_type              varchar(10)                    ,
   zz_reason            varchar(100)                   ,
   zz_jg_dept           varchar(100)                   ,
   eat_drugname_1       varchar(50)                    ,
   eat_drugtype_1       varchar(10)                    ,
   eat_drugdosage_1     varchar(20)                    ,
   eat_drugname_2       varchar(50)                    ,
   eat_drugtype_2       varchar(10)                    ,
   eat_drugdosage_2     varchar(20)                    ,
   eat_drugname_3       varchar(50)                    ,
   eat_drugtype_3       varchar(10)                    ,
   eat_drugdosage_3     varchar(20)                    ,
   health_kind          varchar(20)                    ,
   health_kind_qt       varchar(30)                    ,
   next_sf_date         date                           ,
   input_op             varchar(32)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, xh_no, sf_xh_no)
);

--儿童基本信息表
create table jkda_child_base_info 
(
   b_id                 varchar(30)                    not null,
   father_name          varchar(32)                    ,
   father_pro           varchar(100)                   ,
   father_tel           varchar(15)                    ,
   father_birth         date                           ,
   mother_name          varchar(32)                    ,
   mother_pro           varchar(100)                   ,
   mother_tel           varchar(15)                    ,
   mother_birth         date                           ,
   newborn_asphyxia     int                            ,
   abnormal_flag        int                            ,
   abnormal_memo        varchar(50)                    ,
   newborn_hearing_screen int                          ,
   newborn_weight       decimal(18,1)                  ,
   newborn_height       decimal(18,1)                  ,
   preg_born_no         int                            ,
   feeding_type         int                            ,
   feeding_memo         varchar(100)                   ,
   gest_age_birth       decimal(9,1)                   ,
   preg_sick_tnb        int                            ,
   preg_sick_gxy        int                            ,
   preg_sick_qt         int                            ,
   preg_sick_memo       varchar(100)                   ,
   zc_hosp_name         varchar(50)                    ,
   birth_status         varchar(20)                    ,
   birth_memo           varchar(50)                    ,
   input_op             varchar(32)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   address_sf           varchar(30)                    null,
   address_ds           varchar(30)                    not null,
   address_qx           varchar(30)                    not null,
   address_jd           varchar(30)                    not null,
   address_jwh          varchar(30)                    not null,
   address_group        varchar(30)                    not null,
   address_qt           varchar(50)                    ,
   hosp_id              varchar(6)                     ,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id)
);

--儿童 - 新生儿随访记录表
create table jkda_child_newborn_sfjl 
(
   b_id                 varchar(32)                    not null,
   visit_id             int                            not null,
   visit_date           date                           ,
   visit_dor_name       varchar(32)                    ,
   visit_day_age        int                            ,
   weight               numeric(9,2)                   ,
   ybzk_tw              numeric(9,1)                   ,
   heart_rate           int                            ,
   breath_rate          int                            ,
   pulse_rate           int                            ,
   face_express         varchar(20)                    ,
   face_express_memo    varchar(50)                    ,
   qx_long              numeric(9,1)                   ,
   qx_width             numeric(9,1)                   ,
   qx_flag              int                            ,
   qx_qt_memo           varchar(50)                    ,
   head_circle          numeric(9)                     ,
   eye_flag             int                            ,
   eye_memo             varchar(50)                    ,
   ear_flag             int                            ,
   ear_memo             varchar(50)                    ,
   nose_flag            int                            ,
   nose_memo            varchar(50)                    ,
   mouth_flag           int                            ,
   mouth_memo           varchar(50)                    ,
   heart_flag           int                            ,
   heart_memo           varchar(50)                    ,
   lung_flag            int                            ,
   lung_memo            varchar(50)                    ,
   liver_flag           int                            ,
   liver_memo           varchar(50)                    ,
   spleen_flag          int                            ,
   spleen_memo          varchar(50)                    ,
   belly_flag           int                            ,
   belly_memo           varchar(50)                    ,
   qd_flag              int                            ,
   qd_memo              varchar(50)                    ,
   szhd_flag            int                            ,
   szhd_memo            varchar(50)                    ,
   kgj_flag             int                            ,
   kgj_memo             varchar(50)                    ,
   jbbk_flag            int                            ,
   jbbk_memo            varchar(50)                    ,
   skin_flag            int                            ,
   skin_memo            varchar(50)                    ,
   anus_flag            int                            ,
   anus_memo            varchar(50)                    ,
   wszq_flag            int                            ,
   wszq_memo            varchar(50)                    ,
   spine_flag           int                            ,
   spine_memo           varchar(50)                    ,
   other_memo           varchar(50)                    ,
   zz_flag              int                            ,
   zz_reason            varchar(100)                   ,
   zz_hosp_dept         varchar(50)                    ,
   guide_wyzd_flag      int                            ,
   guide_mrwy_flag      int                            ,
   guide_hlzd_flag      int                            ,
   guide_jbyf_flag      int                            ,
   guide_memo           varchar(50)                    ,
   next_sf_date         date                           ,
   input_op             varchar(32)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     ,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, visit_id)
);

--儿童1~2岁随访表
create table jkda_child_one_two_year 
(
   b_id                 varchar(30)                    not null,
   xm                   int                            not null,
   visit_date           date                           not null,
   visit_dor_name       varchar(32)                    ,
   weight               numeric(18,2)                  ,
   weight_state         varchar(2)                     ,
   height               numeric(18,2)                  ,
   height_state         varchar(2)                     ,
   face_flag            int                             default 0,
   skin_flag            int                             default 0,
   skin_memo            varchar(100)                   ,
   qx_flag              int                             default 0,
   qx_values1           numeric(18,2)                  ,
   qx_values2           numeric(18,2)                   default 0,
   head_circle          numeric(18,2)                  ,
   eye_flag             int                             default 0,
   eye_memo             varchar(100)                   ,
   vision_flag          int                            ,
   vision_memo          varchar(100)                   ,
   ear_flag             int                  ,
   ear_memo             varchar(100)                   ,
   teeth_cnt            int                            ,
   teeths_qc_cnt        int                            ,
   heart_flag           int                            ,
   heart_memo           varchar(100)                   ,
   lung_flag            int                            ,
   lung_memo            varchar(100)                   ,
   flank_flag           int                            ,
   flank_memo           varchar(100)                   ,
   liver_flag           int                            ,
   liver_memo           varchar(100)                   ,
   spleen_flag          int                            ,
   spleen_memo          varchar(100)                   ,
   umbilical_flag       int                            ,
   umbilical_memo       varchar(100)                   ,
   four_limbs_flag      int                            ,
   four_limbs_memo      varchar(100)                   ,
   gait_flag            int                            ,
   gait_memo            varchar(100)                   ,
   hip_flag             int                             default 0,
   hip_memo             varchar(100)                    default '0',
   rickets_flag         varchar(20)                     default '0',
   rickets_memo         varchar(100)                   ,
   rickets_signs        varchar(20)                    ,
   rickets_signs_memo   varchar(100)                   ,
   wszq_flag            int                            ,
   wszq_memo            varchar(100)                   ,
   xhdb_value           numeric(18,2)                  ,
   hwhd_time            numeric(18,2)                  ,
   fywssd_value         numeric(18,2)                  ,
   fyxwpg_flag          int                             default 0,
   lcsfjhbqk            int                            ,
   lcsfjhbqk_memo       varchar(100)                   ,
   other                varchar(200)                    default '0',
   zz_flag              int                            ,
   zz_reason            varchar(100)                   ,
   zz_hosp_dept         varchar(100)                   ,
   zd_flag              varchar(20)                    ,
   zd_memo              varchar(100)                   ,
   next_sf_date         date                           ,
   input_op             varchar(32)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     ,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, xm)
);

--儿童三岁随访记录表
create table jkda_child_three_year 
(
   b_id                 varchar(30)                    not null,
   visit_id             int                            not null,
   visit_date           date                           ,
   visit_dor_name       varchar(50)                    ,
   weight               numeric(9,2)                   ,
   weight_state         varchar(4)                     ,
   height               numeric(9,1)                   ,
   height_state         varchar(4)                     ,
   tgfypj               varchar(50)                    ,
   face_flag            int                            ,
   face_memo            varchar(50)                    ,
   gait_flag            int                            ,
   gait_memo            varchar(50)                    ,
   eye_flag             int                            ,
   eye_memo             varchar(50)                    ,
   ear_flag             int                            ,
   ear_memo             varchar(50)                    ,
   xf_flag              int                            ,
   xf_memo              varchar(50)                    ,
   gp_flag              int                            ,
   gp_memo              varchar(50)                    ,
   fypg_xw              int                            ,
   fypg_xw_memo         varchar(50)                    ,
   fypg_sj              int                            ,
   fypg_sj_memo         varchar(50)                    ,
   yeqhbqk              varchar(50)                    ,
   fy_cs                int                            ,
   fxzy_cs              int                            ,
   wszy_cs              int                            ,
   yeqhbqk_qt           varchar(50)                    ,
   gms_flag             int                            ,
   gms_memo             varchar(50)                    ,
   qt_qk                varchar(200)                   ,
   zz_flag              int                            ,
   zz_reason            varchar(100)                   ,
   zz_hosp_dept         varchar(60)                    ,
   zd_sszd_flag         int                            ,
   zd_yfywsh_flag       int                            ,
   zd_jbyf_flag         int                            ,
   zd_memo              varchar(60)                    ,
   input_op             varchar(32)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     ,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   next_sf_date         date                           ,
   primary key (b_id, visit_id)
);

--孕产妇第一次产前随访记录
create table jkda_preg_base_first 
(
   b_id                 varchar(30)                    not null,
   pregnant_id          int                            not null,
   pregnant_age         int                            ,
   husband_name         varchar(32)                    ,
   husband_age          int                            ,
   husband_tel          varchar(30)                    ,
   gravidity            int                            ,
   parity               int                            ,
   last_menstrual_date  date                           ,
   due_date             date                           ,
   high_risk_flag       int                            ,
   high_risk_type       varchar(50)                    ,
   high_risk_memo       varchar(100)                   ,
   ys_eat_flag          int                            ,
   childbirth_date      date                           ,
   gestational_ages     numeric(9,1)                   ,
   childbirth_type      varchar(20)                    ,
   zc_hosp_name         varchar(50)                    ,
   out_hosp_date        date                           ,
   child_sex            int                            ,
   child_newborn_weight numeric(9)                     ,
   preg_ja_flag         int                            default 0,
   report_date          date                           ,
   reprot_gest_age      numeric(9,1)                   ,
   jws_flag             varchar(30)                    ,
   jws_memo             varchar(200)                   ,
   jzs_ycxjb_flag       int                            ,
   jzs_jsjb_flag        int                            ,
   jzs_qt_flag          int                            ,
   jzs_qt_memo          varchar(100)                   ,
   fk_sss_flag          int                            ,
   fk_sss_memo          varchar(100)                   ,
   ycs_lc               int                            ,
   ycs_st               int                            ,
   ycs_sc               int                            ,
   ycs_xse_sw           int                            ,
   ybzk_height          numeric(9,2)                   ,
   ybzk_weight          numeric(9,2)                   ,
   ybzk_tzzs            numeric(9,2)                   ,
   ybzk_ssy             int                            ,
   ybzk_szy             int                            ,
   tz_xz_flag           int                            ,
   tz_xz_memo           varchar(100)                   ,
   tz_fb_flag           int                            ,
   tz_fb_memo           varchar(100)                   ,
   fkjc_wy_flag         int                            ,
   fkjc_wy_memo         varchar(100)                   ,
   fkjc_yd_flag         int                            ,
   fkjc_yd_memo         varchar(100)                   ,
   fkjc_gj_flag         int                            ,
   fkjc_gj_memo         varchar(100)                   ,
   fkjc_zg_flag         int                            ,
   fkjc_zg_memo         varchar(100)                   ,
   fkjc_fj_flag         int                            ,
   fkjc_fj_memo         varchar(100)                   ,
   fzjc_xcg_xhdb        numeric(18,2)                  ,
   fzjc_xcg_xhdb_memo   varchar(20)                    ,
   fzjc_xcg_bxb         numeric(18,2)                  ,
   fzjc_xcg_bxb_memo    varchar(20)                    ,
   fzjc_xcg_xxb         numeric(18,2)                  ,
   fzjc_xcg_xxb_memo    varchar(20)                    ,
   fzjc_xcg_qt          varchar(100)                   ,
   fzjc_ncg_ndb         varchar(30)                    ,
   fzjc_ncg_nt          varchar(30)                    ,
   fzjc_ncg_ntt         varchar(30)                    ,
   fzjc_ncg_nqx         varchar(30)                    ,
   fzjc_ncg_qt          varchar(100)                   ,
   fzjc_ggn_bzam        numeric(18,2)                  ,
   fzjc_ggn_bzam_memo   varchar(20)                    ,
   fzjc_ggn_czam        numeric(18,2)                  ,
   fzjc_ggn_czam_memo   varchar(20)                    ,
   fzjc_ggn_bdb         numeric(18,2)                  ,
   fzjc_ggn_bdb_memo    varchar(20)                    ,
   fzjc_ggn_zdhs        numeric(18,2)                  ,
   fzjc_ggn_zdhs_memo   varchar(20)                    ,
   fzjc_ggn_jhdhs       numeric(18,2)                  ,
   fzjc_ggn_jhdhs_memo  varchar(20)                    ,
   fzjc_sgn_xqjg        numeric(18,2)                  ,
   fzjc_sgn_xqjg_memo   varchar(20)                    ,
   fzjc_sgn_xnsd        numeric(18,2)                  ,
   fzjc_sgn_xnsd_memo   varchar(20)                    ,
   fzjc_sgn_xjnd        numeric(18,2)                  ,
   fzjc_sgn_xjnd_memo   varchar(20)                    ,
   fzjc_sgn_xnnd        numeric(18,2)                  ,
   fzjc_sgn_xnnd_memo   varchar(20)                    ,
   fzjc_ydfmw_flag      varchar(20)                    ,
   fzjc_ydfmw_memo      varchar(100)                   ,
   fzjc_mdsy_flag       int                            ,
   fzjc_hiv_flag        int                            ,
   all_assess_flag      int                            ,
   all_assess_memo      varchar(200)                   ,
   zz_flag              int                            ,
   zz_reason            varchar(100)                   ,
   zz_hosp_dept         varchar(50)                    ,
   next_sf_date         date                           ,
   sf_dor_name          varchar(32)                    ,
   input_op             varchar(32)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     ,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   address_sf           varchar(30)                    not null,
   address_ds           varchar(30)                    not null,
   address_qx           varchar(30)                    not null,
   address_jd           varchar(30)                    not null,
   address_jwh          varchar(30)                    not null,
   address_group        varchar(30)                    not null,
   address_qt           varchar(50)                    ,
   primary key (b_id, pregnant_id)
);

--孕产妇产前随访服务记录
create table jkda_preg_next_cqsffwjl 
(
   b_id                 varchar(30)                    not null,
   pregnant_id          int                            not null,
   visit_id             int                            not null,
   visit_date           date                           not null,
   visit_dor_name       varchar(32)                    ,
   reprot_gest_age      numeric(18,2)                  ,
   zs_memo              varchar(500)                   ,
   ybzk_weight          numeric(18,2)                  ,
   ckjc_gdgd            numeric(18,2)                  ,
   ckjc_fw              numeric(18,2)                  ,
   ckjc_txl             numeric(18,2)                  ,
   ybzk_ssy             int                            ,
   ybzk_szy             int                            ,
   xhdb_value           numeric(18,2)                  ,
   ndb_value            varchar(100)                   ,
   other                varchar(100)                   ,
   other_bc             varchar(100)                   ,
   other_xtsc           varchar(100)                   ,
   fl_yc_flag           int                            ,
   fl_yc_memo           varchar(100)                   ,
   zd_flag              varchar(20)                    ,
   zz_flag              int                            ,
   zz_reason            varchar(100)                   ,
   zz_hosp_dept         varchar(100)                   ,
   next_sf_date         date                           ,
   input_op             varchar(32)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   zd_memo              varchar(100)                   ,
   hosp_id              varchar(6)                     ,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, pregnant_id, visit_id)
);

--孕产妇产后访视记录表
create table jkda_preg_chfs 
(
   b_id                 varchar(30)                    not null,
   pregnant_id          int                            not null,
   visit_id             int                             default 1,
   visit_date           date                           ,
   visit_dor_name       varchar(50)                    ,
   ybzk_tw              numeric(18,2)                  ,
   ybjkqk               varchar(200)                   ,
   ybxlzk               varchar(100)                   ,
   ybzk_ssy             numeric(18,2)                  ,
   ybzk_szy             numeric(18,2)                  ,
   rf_wjyc              int                            ,
   rf_yc_qk             varchar(50)                    ,
   el_wjyc              int                            ,
   el_yc_qk             varchar(50)                    ,
   zg_wjyc              int                            ,
   zg_yc_qk             varchar(50)                    ,
   sk_wjyc              int                            ,
   sk_yc_qk             varchar(50)                    ,
   qt_qk                varchar(200)                   ,
   fl_wjyc              int                            ,
   fl_yc_qk             varchar(50)                    ,
   zd_list              varchar(20)                    ,
   zz_flag              int                            ,
   zz_reason            varchar(50)                    ,
   zz_hosp_dept         varchar(50)                    ,
   next_sf_date         date                           ,
   input_op             varchar(32)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   zd_memo              varchar(100)                   ,
   hosp_id              varchar(6)                     ,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, pregnant_id, visit_id)
);

--孕产妇产后42天健康检查记录
create table jkda_preg_ch42t_jkjcjlb 
(
   b_id                 varchar(30)                    not null,
   pregnant_id          int                            not null,
   visit_date           date                           ,
   visit_dor_name       varchar(32)                    ,
   ybjkqk               varchar(200)                   ,
   ybxlzk               varchar(200)                   ,
   ybzk_ssy             int                            ,
   ybzk_szy             int                            ,
   rf_yc_flag           int                            ,
   rf_yc_memo           varchar(100)                   ,
   el_yc_flag           int                            ,
   el_yc_memo           varchar(100)                   ,
   zg_yc_flag           int                            ,
   zg_yc_memo           varchar(100)                   ,
   sk_yc_flag           int                            ,
   sk_yc_memo           varchar(100)                   ,
   qt_memo              varchar(200)                   ,
   fl_hf_flag           int                            ,
   fl_hf_memo           varchar(100)                   ,
   zd_flag              varchar(20)                    ,
   zd_qt_memo           varchar(100)                   ,
   cl_flag              int                            ,
   zz_reason            varchar(100)                   ,
   zz_hosp_dept         varchar(100)                   ,
   input_op             varchar(32)                    ,
   input_date           date                       ,
   update_op            varchar(32)                    ,
   update_date          date                       ,
   hosp_id              varchar(6)                     ,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, pregnant_id)
);

--家庭随访记录表
create table jkda_home_visit_detail 
(
   home_lsh             varchar(30)                    not null,
   visit_id             int                            not null,
   reject_flag          varchar(1)                     default '0',
   visit_dor_name       varchar(32)                    ,
   visit_date           date                           not null,
   visit_content        clob                           ,
   input_op             varchar(6)                     ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   hosp_id              varchar(6)                     not null,
   primary key (home_lsh, visit_id)
);

--居民主要用药情况表
create table jkda_person_main_drug 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   dj_date              date                           ,
   input_op             varchar(20)                    ,
   input_date           date                           ,
   drug_name            varchar(100)                   ,
   drug_way             varchar(30)                    ,
   eat_fre              varchar(10)                    ,
   drug_dosage          varchar(30)                    ,
   used_drug_begindate  date                           ,
   used_drug_enddate    date                           ,
   user_roul            varchar(2)                     ,
   drug_memo            varchar(200)                   ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, xh_no)
);

--居民的免疫接种史
create table jkda_person_main_myjzs 
(
   b_id                 varchar(30)                    not null,
   xh_no                int                            not null,
   dj_date              date                           ,
   input_op             varchar(20)                    ,
   input_date           date                           ,
   drug_name            varchar(100)                   ,
   jz_date              date                           ,
   jz_jg                varchar(100)                   ,
   jz_memo              varchar(200)                   ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, xh_no)
);

--高危儿童随访登记表
create table dbo.jkda_child_risk_sfjl 
(
   b_id                 varchar(30)                    not null,
   risk_id              int                            not null,
   sfjl_no              int                            not null,
   visit_date           date                           ,
   visit_dor_name       varchar(32)                    ,
   diag_memo            varchar(200)                   ,
   cheat_method         varchar(300)                   ,
   guide_memo           varchar(300)                   ,
   next_sf_date         date                           ,
   zg_memo              varchar(50)                    ,
   input_op             varchar(32)                    ,
   input_date           datetime                       ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     ,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key  (b_id, risk_id, sfjl_no)
);

--高危儿童专案登记信息表
create table jkda_child_risk_info 
(
   b_id                 varchar(30)                    not null,
   risk_id              int                            not null,
   now_age              int                            ,
   now_agetype          varchar(5)                     ,
   jws_memo             varchar(100)                   ,
   disease_diag         varchar(100)                   ,
   recordin_time        date                           ,
   recordin_diag_memo   varchar(200)                   ,
   recordend_flag       varchar(1)                     default '0',
   recordout_time       date                           ,
   recordout_diag_memo  varchar(200)                   ,
   input_op             varchar(32)                    ,
   input_date           date                           ,
   update_op            varchar(32)                    ,
   update_date          date                           ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                           ,
   primary key (b_id, risk_id)
);

--人员关系字典表
create table jkda_dic_rygx 
(
   bm                   varchar(20)                    not null,
   val                  varchar(20)                    not null,
   gxdj                 varchar(2)                     not null,
   gxdj_name            varchar(50)                    not null,
   py_code              varchar(15)                    ,
   qt_code              varchar(15)                    ,
   sort_no              varchar(5)                     ,
   p_sex                varchar(1)                     ,
   primary key (bm)
);

--民族字典表
create table jkda_dic_nation_code 
(
   nation_code          varchar(2)                     not null,
   nation_name          varchar(24)                    not null,
   py_code              varchar(8)                     ,
   qt_code              varchar(8)                     ,
   nation_no            varchar(3)                     ,
   primary key (nation_code)
);

--基本信息字典表
--婚姻状况: SELECT xm_code ,xm_name FROM jkda_dic_base_info WHERE (type_code = '06')
--文化程序: SELECT xm_code ,xm_name FROM jkda_dic_base_info where type_code ='07'
--职业状况: SELECT xm_code ,xm_name FROM jkda_dic_base_info where type_code ='08'
--血型:     SELECT xm_code ,xm_name FROM jkda_dic_base_info where type_code ='09'
--户口性质: SELECT xm_code ,xm_name FROM jkda_dic_base_info where type_code ='11'
--RH阴性:   1 是,2 否,3 不详
--性别:     1 男 2 女 0 未知性别 9 未说明的性别 
create table jkda_dic_base_info 
(
   xm_code              varchar(6)                     not null,
   xm_name              varchar(100)                   ,
   type_code            varchar(2)                     not null,
   type_name            varchar(50)                    ,
   py_code              varchar(15)                    ,
   qt_code              varchar(15)                    ,
   sort_no              int                            ,
   primary key (xm_code, type_code)
);

--基本字典表,考虑合并
create table jkda_dic_data 
(
   xm_code              int                            not null,
   xm_name              varchar(100)                   not null,
   type_group           varchar(3)                     not null,
   group_memo           varchar(50)                    ,
   sort_no              varchar(6)                     ,
   primary key (xm_code, type_group)
);

--慢病相关的基本信息字典
create table jkda_dic_disease_share_data 
(
   code                 varchar(6)                     not null,
   name                 varchar(50)                    not null,
   mb_type              varchar(6)                     default '%',
   py_code              varchar(30)                    ,
   qt_code              varchar(30)                    ,
   sort_no              varchar(6)                     ,
   type_flag            varchar(10)                    not null,
   memo                 varchar(50)                    ,
   primary key (code, mb_type, type_flag)
);

--慢病和残疾病种编码字典
create table jkda_dic_disease_disability 
(
   id_no                varchar(6)                     not null,
   t_name               varchar(50)                    not null,
   t_flag               varchar(1)                     default '0',
   py_code              varchar(50)                    ,
   qt_code              varchar(50)                    ,
   primary key (id_no)
);

---慢病SOAP模板字典表
create table jkda_dic_disease_soap_pattern 
(
   pat_code             varchar(6)                     not null,
   pat_name             varchar(30)                    null,
   dis_name             varchar(100)                   not null,
   s_zgzl               clob                           not null,
   o_kgzl               clob                           not null,
   a_pg                 clob                           not null,
   p_jh                 clob                           not null,
   py_code              varchar(15)                    null,
   qt_code              varchar(15)                    null,
   share_flag           varchar(1)                     default '0',
   op_code              varchar(6)                     not null,
   hosp_id              varchar(6)                     null,
   primary key(pat_code)
);

--健康教育模板字典
create table jkda_dic_heath_edu_pattern 
(
   pat_code             varchar(6)                     not null,
   pat_name             varchar(30)                    not null,
   xj_memo              clob                           null,
   zx_memo              clob                           null,
   zz_memo              clob                           null,
   py_code              varchar(15)                    null,
   qt_code              varchar(15)                    null,
   share_flag           varchar(1)                     null,
   op_code              varchar(20)                    null,
   hosp_id              varchar(6)                     null,
   primary key(pat_code)
);

--住院问题相关的住院记录
create table jkda_person_mainpro_zylist 
(
   b_id                 varchar(30)                    not null,
   w_xh_no              int                            not null,
   xh_no                int                            not null,
   zys_ry_date          date                       		,
   zys_cy_date          date                      		,
   zys_zy_reason        varchar(200)                   ,
   zys_yljgmc           varchar(100)                   ,
   zys_bah              varchar(50)                    ,
   memo                 varchar(200)                   ,
   input_op             varchar(20)                    ,
   input_date           date                       ,
   update_op            varchar(32)                    ,
   update_date          date                       ,
   hosp_id              varchar(6)                     not null,
   check_flag           int                            default 1,
   check_op             varchar(6)                     ,
   check_date           date                       ,
   primary key(b_id, w_xh_no, xh_no)
);

--添加列
alter table JKDA_CJGL_ADD_PERSON_INFO add hosp_id varchar(6);




---2011-12-14-----
--新增列
alter table jkda_person_base_info add life_fan int default -1;
alter table jkda_person_base_info add life_firing int default -1;
alter table jkda_person_base_info add life_water int default -1;
alter table jkda_person_base_info add life_wc int default -1;
alter table jkda_person_base_info add life_lsbar int default -1;
                                      
alter table jkda_person_main_myjzs add jc int default -1;
alter table jkda_person_main_myjzs add jz_bw varchar(200);
alter table jkda_person_main_myjzs add jz_ph varchar(20);
alter table jkda_person_main_myjzs add jz_dor varchar(20);
                                       
--预防接种卡
create table Vaccination_Card 
(
   Abnormal_His         varchar2(200)                  ,
   Vac_Taboo            varchar2(200)                  ,
   infectious_his       varchar2(200)                  ,
   create_date          date                           ,
   create_opr           varchar(30)                    ,
   Bid                  varchar(30)                    not null,
   Guardian_name        varchar(30)                    ,
   child_rel            varchar(30)                    ,
   telphone             varchar(30)                    ,
   hosp_id              varchar(6)                     ,
   primary key (Bid)
);

--疫苗免疫程序
create table Vaccine_Dic 
(
   Vaccine_Name         varchar2(30)                   ,
   month_age            varchar2(30)                   ,
   vac_Doses            int                            ,
   vac_Position         varchar2(30)                   ,
   vac_Way              varchar2(30)                   ,
   vac_potion           varchar2(100)                  ,
   hosp_id              varchar(6)                     ,
   memo                 varchar2(500)                  
);

--儿童 - 新生儿随访记录表
alter table jkda_child_newborn_sfjl add feeding_Amount float;
alter table jkda_child_newborn_sfjl add feeding_times int;
alter table jkda_child_newborn_sfjl add Vomit int;
alter table jkda_child_newborn_sfjl add Defecate int;
alter table jkda_child_newborn_sfjl add Defecate_times int;
alter table jkda_child_newborn_sfjl add disease_screen int;
alter table jkda_child_newborn_sfjl add disease_screen_other varchar(100);

--儿童三岁随访记录表
alter table jkda_child_three_year add month_age int;
alter table jkda_child_three_year add vision    int;
alter table jkda_child_three_year add hearing   int;
alter table jkda_child_three_year add teeth_num varchar(20);
alter table jkda_child_three_year add Hemoglobin float;       
    

--孕产妇第一次产前随访记录  
alter table jkda_preg_base_first add personal_history varchar2(50);
alter table jkda_preg_base_first add personal_history_other varchar2(200);
alter table jkda_preg_base_first add fz_abo varchar2(50);
alter table jkda_preg_base_first add fz_rh varchar2(50);
alter table jkda_preg_base_first add fz_xt float;
alter table jkda_preg_base_first add fz_ydqjd int;
alter table jkda_preg_base_first add fz_yg_wx1 int;
alter table jkda_preg_base_first add fz_yg_wx2 int;
alter table jkda_preg_base_first add fz_yg_wx3 int;
alter table jkda_preg_base_first add fz_yg_wx4 int;
alter table jkda_preg_base_first add fz_yg_wx5 int;
alter table jkda_preg_base_first add fz_bc varchar2(200);
alter table jkda_preg_base_first add bjzd varchar2(50);
alter table jkda_preg_base_first add bjzd_other varchar2(200);

--孕产妇产前随访服务记录
alter table jkda_preg_next_cqsffwjl add Fetal varchar(20);

--病情流程表 新增字段
alter table jkda_slow_dis_bqlc_gxb add sffs_Type VARCHAR2(20);
alter table jkda_slow_dis_bqlc_exzl add sffs_Type VARCHAR2(20);
alter table jkda_slow_dis_bqlc_mzfb add sffs_Type VARCHAR2(20);
alter table jkda_slow_dis_bqlc_nxg add sffs_Type VARCHAR2(20);
alter table jkda_slow_dis_bqlc_zhbz add sffs_Type VARCHAR2(20);
alter table jkda_cjgl_person_sfjl add sffs_type varchar2(20);

--老年人生活自理能力评估表
create table SelfCareEvaluate 
(
   meal_score           int                            ,
   freshen_score        int                            ,
   cloth_score          int                            ,
   wc_score             int                            ,
   activity_score       int                            ,
   score                int                            ,
   Bid                  varchar(30)                    not null,
   hosp_id              varchar(6)                     
);

comment on table SelfCareEvaluate is 
'0-3分为可自理
4-8分为轻度依赖
9-18分为中度依赖
19分为不能自理';

--残疾患者个人信息
alter table jkda_cjgl_add_person_info add agree_type int;
alter table jkda_cjgl_add_person_info add agree_sign varchar2(20);
alter table jkda_cjgl_add_person_info add agree_sign_time date;
alter table jkda_cjgl_add_person_info add Economic_kind int;
alter table jkda_cjgl_add_person_info add dor_suggest varchar2(200);

--残疾患者随访记录
alter table jkda_cjgl_person_sfjl add danger_type int;
alter table jkda_cjgl_person_sfjl add lock_kind int;
alter table jkda_cjgl_person_sfjl add inhospital_kind int;
alter table jkda_cjgl_person_sfjl add last_leavehosp_date date;

--卫生监督信息报告登录表
create table HealthReportReg 
(
   NoId                 NUMBER(9)                      ,
   Find_Time            date                           ,
   Info_Type            int                            ,
   Info_Content         varchar2(2000)                 ,
   Report_Time          date                           ,
   Reporter             varchar2(20)                   ,
   hosp_id              varchar(6)                     
);

comment on column HealthReportReg.Info_Type is 
'食品安全
饮用水卫生
职业病危害
学校卫生
非法行医';                                                                      

--卫生监督巡查登记表
create table HealthCheckReg 
(
   NoId                 number(9)                      ,
   Check_Area_Content   varchar2(200)                  ,
   Main_Problem         varchar2(2000)                 ,
   Check_Date           date                           ,
   Checker              varchar2(20)                   ,
   Memo                 varchar2(200)                  ,
   hosp_id              varchar(6) 
);


---居民周期性检查 - 一般状况
alter table jkda_person_tj_ybzk add health_state int;
alter table jkda_person_tj_ybzk add SelfCareEvaluate numeric(9,2);

---普通居民随访表
CREATE TABLE jkda_person_tj_sfjl (
	b_id varchar2(30)   NOT NULL ,
	xh_no int NOT NULL ,
	sfrq date NOT NULL ,
	zr_dor varchar2(20) ,
	sffs_type varchar2(20) ,
	bqzz varchar2(200) ,
	ssy int ,
	szy int ,
	weight numeric(18, 2)  ,
	weight_next numeric(18, 2)  ,
	tzzs numeric(18, 2)  ,
	height numeric(18, 2)  ,
	heart_rate int ,
	heart_rate_next int ,
	other varchar2(100) ,
	day_smoking numeric(9, 2)  ,
	day_smoking_next numeric(9, 2)  ,
	day_drinking numeric(9, 2)  ,
	day_drinking_next numeric(9, 2)  ,
	week_sport numeric(9, 2)  ,
	week_sport_next numeric(9, 2)  ,
	sport_long numeric(9, 2)  ,
	sport_long_next numeric(9, 2)  ,
	day_salt numeric(9, 2)  ,
	day_salt_next numeric(9, 2)  ,
	xltz_type varchar2(20) ,
	zyxw_type varchar2(20) ,
	kfxt_value numeric(18, 2)  ,
	thxhdb_value numeric(18, 2)  ,
	thxhdb_check_date date ,
	fzjc_memo varchar2(1000) ,
	fyycx_flag varchar2(20) ,
	drug_blfy_flag varchar2(1) ,
	drug_blfy_memo varchar2(200) ,
	this_sf_effect varchar2(50) ,
	eat_drugname_1 varchar2(50) ,
	eat_drugtype_1 varchar2(10) ,
	eat_drugdosage_1 varchar2(20) ,
	eat_drugname_2 varchar2(50) ,
	eat_drugtype_2 varchar2(10) ,
	eat_drugdosage_2 varchar2(20) ,
	eat_drugname_3 varchar2(50) ,
	eat_drugtype_3 varchar2(10) ,
	eat_drugdosage_3 varchar2(20) ,
	eat_drugname_qt varchar2(200) ,
	zz_reason varchar2(100) ,
	zz_hosp_and_dept varchar2(100) ,
	next_sf_date date ,
	hosp_id varchar2(6)   NOT NULL ,
	check_flag int DEFAULT 1  NOT NULL,
	check_op varchar2(6) ,
	check_date date ,
	input_op varchar2(10)   NOT NULL ,
	input_date date,
	update_op varchar2(32) ,
	update_date date ,
	primary key(b_id,xh_no)
) ;

--分管地区
alter table WEB_OPER_MANAGE_AREA add ORG_ID int;
alter table WEB_OPER_MANAGE_AREA modify ADDR_NAME null;

--消息公告
drop table WEB_NOTICE;
create table WEB_NOTICE  (
   ORG_ID               INT,
   NID                  INT,
   N_TIME               date,
   TITLE                VARCHAR(50),
   CONTENT              VARCHAR(2048),
   PUBLISHER_ID         NUMBER(8),
   PUBLISHER_NAME       VARCHAR(32),
   HITS                 INT,
   N_TYPE               INT,
   ATTACH_URL           VARCHAR(300),
   primary key (NID)
);

--消息回复
create table WEB_NOTICE_REPLY  (
   NID                  INT,
   R_NID                INT,
   R_OPRID              NUMBER(8),
   R_OPRNAME            VARCHAR(32),
   R_TIME               date,
   R_CONTENT            VARCHAR2(2048),
   R_TITLE              VARCHAR2(50),
   primary key (R_NID)
);
create sequence SEQ_RNOTICEID minvalue 1 maxvalue 999999999999999999999999999 start with 1 increment by 1 ;


--报表配置
create table WEB_REP_CFG  (
   REP_ID               INT                             not null,
   REP_NAME             VARCHAR2(50),
   ADD_OPRID            INT,
   ADD_TIME             DATE,
   STATUS               INT,
   MODIFY_TIME          DATE,
   MODITY_OPRID         INT,
   REP_SQL              CLOB,
   REP_NO               INT,
   REP_TYPE             INT,
   primary key (REP_ID)
);

comment on column WEB_REP_CFG.STATUS is
'0 无效 1 有效';

comment on column WEB_REP_CFG.REP_TYPE is
'0 日 1 月';

create table WEB_REP_LOG  (
   REP_ID               INT,
   RUN_SQL              CLOB,
   RUN_DATE             DATE,
   RUN_START_TIME       DATE,
   RUN_END_TIME         DATE,
   TIMES                NUMBER(9,2)
);

--儿童 - 新生儿随访记录表
alter table jkda_child_newborn_sfjl add zd_flag varchar2(20);
alter table jkda_child_newborn_sfjl add next_sf_area varchar(50);

--儿童1~2岁随访表
alter table jkda_child_one_two_year add hear_flag int;
alter table jkda_child_one_two_year add abdomen int;

--儿童三岁随访记录表 
alter table jkda_child_three_year add abdomen int; 
alter table jkda_child_three_year add zd_flag varchar2(20);

--儿童基本信息表
alter table jkda_child_base_info add newborn_disease_flag int default 0;
alter table jkda_child_base_info add newborn_disease_other varchar2(100);


----
create table REP_DIS_COMPOSE  (
   AGE_GROUP            INT,
   DIS_TYPE             VARCHAR(10),
   DIS_DATE             DATE,
   SEX                  CHAR(1),
   DIS_NUM              NUMBER(9),
   ALL_NUM              NUMBER(9),
   HOSP_ID              varchar(6),
   PERSON_NUM						NUMBER(9)
);
alter table REP_DIS_COMPOSE add PERSON_NUM NUMBER(9);

create table REP_WORKLOAD  (
   REP_DATE             DATE,
   WORK_TYPE            INT,
   PERSON_TYPE          INT,
   WORKLOAD             NUMBER(9),
   HOSP_ID              VARCHAR(6)
);

create table WEB_REP_CFG  (
   REP_ID               INT                             not null,
   REP_NAME             VARCHAR2(50),
   ADD_OPRID            INT,
   ADD_TIME             DATE,
   STATUS               INT,
   MODIFY_TIME          DATE,
   MODITY_OPRID         INT,
   REP_SQL              CLOB,
   REP_NO               INT,
   REP_TYPE             INT,
   constraint PK_WEB_REP_CFG primary key (REP_ID)
);

create table WEB_REP_LOG  (
   REP_ID               INT,
   RUN_SQL              CLOB,
   RUN_DATE             VARCHAR2(20),
   RUN_START_TIME       DATE,
   RUN_END_TIME         DATE,
   TIMES                NUMBER(9,2)
);


alter table WEB_ORG_INFO add P_ORG_ID int default -1;
alter table WEB_NOTICE add RECIVE_LIST_ID VARCHAR(2048);
alter table WEB_ORG_INFO add ORG_TYPE int default 1;

alter table WEB_NOTICE add ATTACH_NAME VARCHAR(2048);
alter table WEB_NOTICE modify ATTACH_URL VARCHAR2(4000);
create index ind_web_noticeread_01 on web_notice_read(nid,opr_id);
create index ind_web_notice_01 on WEB_NOTICE(N_TYPE);
create index ind_web_notice_reply_01 on WEB_NOTICE_REPLY(NID);

alter table WEB_OPR_LOGIN_LOG add AGENT_PIXEL VARCHAR2(50);
alter table WEB_OPR_LOGIN_LOG add AGENT_TYPE INT;
alter table WEB_OPR_LOGIN_LOG add OS VARCHAR2(128);

create table SMS_LIST  (
   OPR_ID               NUMBER(8),
   REV_PHONE_LIST       VARCHAR2(1000),
   REV_OPRID_LIST       VARCHAR2(500),
   SMS_CONTENT          VARCHAR2(150),
   IS_SEND              INT,
   ADD_TIME             DATE,
   SEND_TIME            DATE,
   CHARGE               INT,
   SMS_TYPE             INT,
   SMS_ID               INT    not null,
   primary key (SMS_ID)
);

comment on column SMS_LIST.IS_SEND is
'0 没有发送 1 已发送';

comment on column SMS_LIST.CHARGE is
'单位:分';

comment on column SMS_LIST.SMS_TYPE is
'1 SMS
2 MMS
3 WAPPUSH';

drop table SENDMSG cascade constraints;
create table SENDMSG  (
   MSGINDEX             INT                             not null,
   PHONENUMBER          VARCHAR(250),
   MSGTITLE             VARCHAR(250),
   MMSINFOFILE          VARCHAR(250),
   TIMESEND             VARCHAR(100),
   MSGTYPE              INT,
   MSGSTATUS            INT,
   SENTTIME             VARCHAR(50),
   RUNINFO              VARCHAR(250),
   SENDREPORT           VARCHAR(50),
   SERVERMSGID          INT,
   SENDMODEM            VARCHAR(50),
   IMAGEWIDTH           INT,
   IMAGEHEIGHT          INT,
   AUTOCOMPRESS         INT,
   NUMBERCOUNT          INT,
   RESENDTIMES          INT,
   OLDMSGINDEX          INT,
   ID                   INT,
   constraint PK_SENDMSG primary key (MSGINDEX)
);

comment on table SENDMSG is
'短信池';

comment on column SENDMSG.MSGINDEX is
'消息编号';

comment on column SENDMSG.PHONENUMBER is
'手机号码';

comment on column SENDMSG.MSGTITLE is
'短消息标题';

comment on column SENDMSG.MMSINFOFILE is
'MMS标题';

comment on column SENDMSG.TIMESEND is
'发送用时';

comment on column SENDMSG.MSGTYPE is
'消息类型:0 短信 1 wappush';

comment on column SENDMSG.MSGSTATUS is
'消息状态:0 未发送 1已发送';

comment on column SENDMSG.SENTTIME is
'发送时间';

comment on column SENDMSG.RUNINFO is
'运行信息';

comment on column SENDMSG.SENDREPORT is
'发送报告';

comment on column SENDMSG.SERVERMSGID is
'服务端消息编号';

comment on column SENDMSG.SENDMODEM is
'发送MODEM';

comment on column SENDMSG.IMAGEWIDTH is
'图片宽度';

comment on column SENDMSG.IMAGEHEIGHT is
'图片高度';

comment on column SENDMSG.AUTOCOMPRESS is
'自动压缩';

comment on column SENDMSG.NUMBERCOUNT is
'号码数量';

comment on column SENDMSG.RESENDTIMES is
'重复发送次数';

comment on column SENDMSG.OLDMSGINDEX is
'旧消息编号';

comment on column SENDMSG.ID is
'编号';


--隐藏数据扩展
alter table WEB_ORG_INFO add FUN_PARAM varchar(50) default '';
alter table jkda_person_tj_ct   add hide_flag INTEGER default 0;
alter table jkda_person_tj_zqgn add hide_flag INTEGER default 0;
alter table jkda_person_tj_fzjc add hide_flag INTEGER default 0;
alter table jkda_person_tj_ybzk add hide_flag INTEGER default 0;

update web_org_info set fun_param='0,0,0';

--数据变更
alter table jkda_person_tj_fzjc add fzjc_sjxt numeric(18,2);
alter table jkda_person_tj_fzjc add fzjc_sjxt_memo varchar(20);

--导数据,更新数据
update web_opr_info a set org_id = (select org_id from web_org_info b where a.org_id=b.org_code) 
where a.opr_id>=100 and exists (select 1 from web_org_info c where a.org_id=c.org_code);
update web_opr_info set creator =2 where creator is null;
update web_opr_info set create_date = sysdate where create_date is null;
update web_opr_info set active_date = sysdate where active_date is null;

--地区表数据更新
update web_address a1 set org_id = (select org_id  from 
(
select a.addr_id,b.org_id from jkda_test.web_address a,web_org_info b where a.org_id=b.org_code
) a2
 where a1.addr_id=a2.addr_id 
)

update web_address set sort_order=1 where sort_order is null;

--用户配置 表
create table WEB_OPR_CONFIG  (
   OPR_ID               NUMBER(8)                       not null,
   CONFIG_KEY           VARCHAR(100)                    not null,
   CONFIG_VALUE         VARCHAR(500),
   constraint PK_WEB_OPR_CONFIG primary key (OPR_ID, CONFIG_KEY)
);

comment on table WEB_OPR_CONFIG is
'用户个性化定制的配置信息';

alter table WEB_OPR_CONFIG
   add constraint FK_WEB_OPR__REFERENCE_WEB_OPR_ foreign key (OPR_ID)
      references WEB_OPR_INFO (OPR_ID);
      
--生活方式升级
alter table jkda_person_tj_shfs add shfs_work_type_fc      varchar(50);
alter table jkda_person_tj_shfs add shfs_work_type_fc_fh   varchar(1);
alter table jkda_person_tj_shfs add shfs_work_type_fc_fhcs varchar(50);
alter table jkda_person_tj_shfs add shfs_work_type_wlys      varchar(50);
alter table jkda_person_tj_shfs add shfs_work_type_wlys_fh   varchar(1);
alter table jkda_person_tj_shfs add shfs_work_type_wlys_fhcs varchar(50);
alter table jkda_person_tj_shfs add shfs_work_type_other      varchar(50);
alter table jkda_person_tj_shfs add shfs_work_type_other_fh   varchar(1);
alter table jkda_person_tj_shfs add shfs_work_type_other_fhcs varchar(50);

--母乳喂养随访
create table JKDA_CHILD_FEED_SF 
(
   B_ID                 varchar(30)                    not null,
   VISIT_DATE           date                           null,
   DAYS                 int                            not null,
   FEED_TYPE            int                            null,
   ASSIST_FOOD          int                            null,
   WEIGHT               decimal(18,1)                  null,
   HEALTH_STATE         varchar(200)                   null,
   VISIT_DOR_NAME       varchar(50)                    null,
   INPUT_OP             varchar(32)                    null,
   INPUT_DATE           date                           null,
   UPDATE_OP            varchar(32)                    null,
   UPDATE_DATE          date                           null,
   HOSP_ID              varchar(6)                     null,
   CHECK_FLAG           int                            default 1,
   CHECK_OP             varchar(6)                     null,
   CHECK_DATE           date                           null,
   NEXT_VISIT_DATE      date                           null,
   primary key (B_ID, DAYS)
);

comment on column JKDA_CHILD_FEED_SF.DAYS is 
'7天
14天
28天
1~2个月
2~3个月
3~4个月
4~6个月';

comment on column JKDA_CHILD_FEED_SF.FEED_TYPE is 
'1 纯母乳喂养
2 几乎纯母乳喂养
3 部分纯母乳喂养
4 人工喂养';

comment on column JKDA_CHILD_FEED_SF.ASSIST_FOOD is 
'1 鲜牛奶
2 奶粉
3 米糊
4 粥';

alter table jkda_slow_dis_bqlc_gxy  add ybzk_weight_next numeric(18,2);

alter table jkda_person_tj_ct add fzjc_yd_flag varchar(1) default '';
alter table jkda_person_tj_ct add fzjc_yd_memo varchar(200);

alter table jkda_person_tj_fzjc add fzjc_xcg_hxb numeric(18,2);
alter table jkda_person_tj_fzjc add fzjc_xcg_hxb_memo varchar(20);

update web_dim set dim_code='3'  where DIM_CLASS='NL' and dim_name='3岁';
update web_dim set dim_code='4'  where DIM_CLASS='NL' and dim_name='4岁';
update web_dim set dim_code='5'  where DIM_CLASS='NL' and dim_name='5岁';
update web_dim set dim_code='6'  where DIM_CLASS='NL' and dim_name='6岁';

--报表配置表
create table jkda_slow_dis_age_section
(
	sort_code int ,
	begin_age int  ,
	end_age int ,
	section_memo varchar(30)
)  ;

insert into jkda_slow_dis_age_section(sort_code,begin_age,end_age,section_memo)
values(1,0,1,'0-1');
insert into jkda_slow_dis_age_section(sort_code,begin_age,end_age,section_memo)
values(2,1,5,'1-5');
insert into jkda_slow_dis_age_section(sort_code,begin_age,end_age,section_memo)
values(3,5,10,'5-10');
insert into jkda_slow_dis_age_section(sort_code,begin_age,end_age,section_memo)
values(4,10,20,'10-20');
insert into jkda_slow_dis_age_section(sort_code,begin_age,end_age,section_memo)
values(5,20,30,'20-30');
insert into jkda_slow_dis_age_section(sort_code,begin_age,end_age,section_memo)
values(6,30,40,'30-40');
insert into jkda_slow_dis_age_section(sort_code,begin_age,end_age,section_memo)
values(7,40,50,'40-50');
insert into jkda_slow_dis_age_section(sort_code,begin_age,end_age,section_memo)
values(8,50,60,'50-60');
insert into jkda_slow_dis_age_section(sort_code,begin_age,end_age,section_memo)
values(9,60,65,'60-65');
insert into jkda_slow_dis_age_section(sort_code,begin_age,end_age,section_memo)
values(10,65,70,'65-70');
insert into jkda_slow_dis_age_section(sort_code,begin_age,end_age,section_memo)
values(11,70,80,'70-80');
insert into jkda_slow_dis_age_section(sort_code,begin_age,end_age,section_memo)
values(12,80,200,'80以上');

--视图
create or replace view jkda_slow_dis_bqlc as
select b.home_lsh,e.home_tel,b.b_id,b.person_name,b.p_sex,b.p_birth,b.p_age,b.p_tel,a.visit_date, c.t_name,d.w_id_no,b.hosp_id,b.address_jwh
from jkda_slow_dis_bqlc_gxy a,jkda_person_base_info b,jkda_person_main_problem c,jkda_dic_health_problems d ,jkda_home_base_info e
where a.b_id=b.b_id and  e.home_lsh=b.home_lsh and c.b_id=a.b_id and c.xh_no=a.pro_xh_no and c.rej_manage_flag<>'0' and c.t_type='01'
 and c.t_type=d.t_type and c.t_id_no=d.id_no and d.w_id_no in ('M001')
union
select b.home_lsh,e.home_tel,b.b_id,b.person_name,b.p_sex,b.p_birth,b.p_age,b.p_tel,a.visit_date, c.t_name,d.w_id_no,b.hosp_id,b.address_jwh
from jkda_slow_dis_bqlc_nxg a,jkda_person_base_info b,jkda_person_main_problem c,jkda_dic_health_problems d ,jkda_home_base_info e
where a.b_id=b.b_id and  e.home_lsh=b.home_lsh and c.b_id=a.b_id and c.xh_no=a.pro_xh_no and c.rej_manage_flag<>'0' and c.t_type='01'
 and c.t_type=d.t_type and c.t_id_no=d.id_no and d.w_id_no in ('M002')
union
select b.home_lsh,e.home_tel,b.b_id,b.person_name,b.p_sex,b.p_birth,b.p_age,b.p_tel,a.visit_date, c.t_name,d.w_id_no,b.hosp_id,b.address_jwh
from jkda_slow_dis_bqlc_tnb a,jkda_person_base_info b,jkda_person_main_problem c,jkda_dic_health_problems d ,jkda_home_base_info e
where a.b_id=b.b_id and  e.home_lsh=b.home_lsh and c.b_id=a.b_id and c.xh_no=a.pro_xh_no and c.rej_manage_flag<>'0' and c.t_type='01'
 and c.t_type=d.t_type and c.t_id_no=d.id_no and d.w_id_no in ('M003')
union
select b.home_lsh,e.home_tel,b.b_id,b.person_name,b.p_sex,b.p_birth,b.p_age,b.p_tel,a.visit_date, c.t_name,d.w_id_no,b.hosp_id,b.address_jwh
from jkda_slow_dis_bqlc_gxb a,jkda_person_base_info b,jkda_person_main_problem c,jkda_dic_health_problems d ,jkda_home_base_info e
where a.b_id=b.b_id and  e.home_lsh=b.home_lsh and c.b_id=a.b_id and c.xh_no=a.pro_xh_no and c.rej_manage_flag<>'0' and c.t_type='01'
 and c.t_type=d.t_type and c.t_id_no=d.id_no and d.w_id_no in ('M004')
union
select b.home_lsh,e.home_tel,b.b_id,b.person_name,b.p_sex,b.p_birth,b.p_age,b.p_tel,a.visit_date, c.t_name,d.w_id_no,b.hosp_id,b.address_jwh
from jkda_slow_dis_bqlc_exzl a,jkda_person_base_info b,jkda_person_main_problem c,jkda_dic_health_problems d ,jkda_home_base_info e
where a.b_id=b.b_id and  e.home_lsh=b.home_lsh and c.b_id=a.b_id and c.xh_no=a.pro_xh_no and c.rej_manage_flag<>'0' and c.t_type='01'
 and c.t_type=d.t_type and c.t_id_no=d.id_no and d.w_id_no in ('M005')
union
select b.home_lsh,e.home_tel,b.b_id,b.person_name,b.p_sex,b.p_birth,b.p_age,b.p_tel,a.visit_date, c.t_name,d.w_id_no,b.hosp_id,b.address_jwh
from jkda_slow_dis_bqlc_mzfb a,jkda_person_base_info b,jkda_person_main_problem c,jkda_dic_health_problems d ,jkda_home_base_info e
where a.b_id=b.b_id and  e.home_lsh=b.home_lsh and c.b_id=a.b_id and c.xh_no=a.pro_xh_no and c.rej_manage_flag<>'0' and c.t_type='01'
 and c.t_type=d.t_type and c.t_id_no=d.id_no and d.w_id_no in ('M006')



--性能优化
create index ind_js_dimensions_01 on js_dimensions(OUT_NAME);
create index ind_webdim_01 on WEB_DIM(DIM_CLASS);
create index ind_webdim_02 on WEB_DIM(DIM_CODE);
create index ind_jkda_dic_base_info_01 on jkda_dic_base_info(type_code);
create index ind_WEB_ADDRESS_01 on WEB_ADDRESS(type_id);
create index ind_WEB_ADDRESS_02 on WEB_ADDRESS(PADDR_ID);
create index ind_WEB_ADDRESS_03 on WEB_ADDRESS(ORG_ID);
create index ind_WEB_ORG_INFO_01 on WEB_ORG_INFO(P_ORG_ID);
create index ind_dic_disease_share_data_01 on jkda_dic_disease_share_data(mb_type,type_flag);
create index ind_jkda_dic_data_01 on jkda_dic_data(type_group);
create index ind_WEB_OPR_INFO_01 on WEB_OPR_INFO(opr_login_name);
create index ind_WEB_OPR_INFO_02 on WEB_OPR_INFO(opr_login_name,Opr_Login_Pwd);
create index ind_dic_health_problems_01 on jkda_dic_health_problems(w_id_no);
create index ind_dic_health_problems_02 on jkda_dic_health_problems(t_type);

create index ind_WEB_RES_INFO_01 on WEB_RES_INFO(P_RES_ID);
create index ind_WEB_RES_INFO_02 on WEB_RES_INFO(RES_TYPE);
create index ind_WEB_ROLE_2_RES_01 on WEB_ROLE_2_RES(RES_ID);
create index ind_WEB_ROLE_2_RES_02 on WEB_ROLE_2_RES(ROLE_ID);
create index ind_WEB_OPR_2_ROLE_01 on WEB_OPR_2_ROLE(OPR_ID);
create index ind_WEB_OPR_2_ROLE_02 on WEB_OPR_2_ROLE(ROLE_ID);

create index ind_PERSON_TJ_TJJL_01 on JKDA_PERSON_TJ_TJJL(B_ID);

--查表占用空间大小
Select Segment_Name,Sum(bytes)/1024/1024 From User_Extents Group By Segment_Name;

--增加表空间大小
select  b.tablespace_name 表空间名称,  round(total_space / 1024 / 1024) "总共M",round((total_space - nvl(free_space,0)) / 1024 / 1024) UseM,
 round(nvl(free_space,0) / 1024 / 1024) FreeM,trunc((nvl(free_space,0) / total_space) * 100)  空闲率
    from (select tablespace_name, sum(bytes) free_space  from dba_free_space group by tablespace_name) a,
         (select tablespace_name, sum(bytes) total_space from dba_data_files group by tablespace_name) b
   where a.tablespace_name(+) = b.tablespace_name 
    order by 空闲率  ;
alter tablespace DATA01_HIS add datafile 'E:\JKDA_DATA\DATA01_HIS_01.DBF' size 2048m;

--建表空间
CREATE TABLESPACE DATA_JKDA 
       DATAFILE 'E:\JKDA_DATA\DATA_JKDA.DBF' SIZE 10240M EXTENT MANAGEMENT LOCAL
       SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE 
       BLOCKSIZE 8k ;
--建分区表空间
CREATE TABLESPACE QDATA_JKDA_A110 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A110.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A004 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A004.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A106 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A106.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A109 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A109.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A115 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A115.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A119 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A119.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A104 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A104.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A108 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A108.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A113 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A113.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A003 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A003.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A005 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A005.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A007 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A007.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A008 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A008.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A112 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A112.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A116 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A116.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A002 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A002.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A006 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A006.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A103 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A103.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A105 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A105.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A117 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A117.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A111 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A111.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A107 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A107.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A118 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A118.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_ALL  DATAFILE 'E:\JKDA_DATA\DATA_JKDA_ALL.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
CREATE TABLESPACE QDATA_JKDA_A114 DATAFILE 'E:\JKDA_DATA\DATA_JKDA_A114.DBF' SIZE 4096M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO AUTOALLOCATE BLOCKSIZE 8k ;
--修改用户默认表空间
alter user JKDA_WEB
  default tablespace DATA_JKDA;
       
--重建表
select 'drop table JKDA_WEB.'||TABLE_NAME||';' from all_tables where owner='JKDA_WEB';
     
       
--分析,一般blocksize=8k够用,此时datafile最大16G
--windows 查block size
>fsutil fsinfo ntfsinfo e:
Bytes Per Cluster :               4096

       
--新增用户
create user jkda_web
  identified by admin
  default tablespace DATA01_HIS;
-- Grant/Revoke role privileges 
grant dba to JKDA_WEB;
-- Grant/Revoke system privileges 
grant unlimited tablespace to JKDA_WEB;          

--解锁
ALTER USER jkda_test ACCOUNT UNLOCK;
select * from dba_users a, dba_profiles b where a.profile=b.profile and a.username = 'JKDA_TEST'


--扩展函数
  FUNCTION IsInStr(STR1 VARCHAR2,STR2 VARCHAR2)
     RETURN integer
     IS
        l_arrvol SOURCE_ARRAY;
        l_col_index number;  
        ret integer;
     BEGIN
          ret := 0;
          l_arrvol := GET_COLARRAY(STR1,',');
          for l_col_index in 1..l_arrvol.count
           loop
               if l_arrvol(l_col_index)=STR2 then 
                  ret := 1;
                  exit ;
               end if;
           end loop;
           return ret;
     EXCEPTION
     when others then
        return  0;
      
  END;

FUNCTION GET_COLCOUNT(COLS VARCHAR2,SPLIT_CHAR VARCHAR2)
RETURN NUMBER
IS
l_rtn number;
BEGIN
  if COLS is NULL or TRIM(COLS)||'A' = 'A' then
      return 0;
    end if;
  l_rtn := 1;
    WHILE INSTR(COLS,SPLIT_CHAR,1,l_rtn) > 0
    LOOP
      l_rtn := l_rtn + 1;
    END LOOP;
  RETURN l_rtn;
END;

FUNCTION GET_COLCODE(COLS VARCHAR2,SPLIT_CHAR VARCHAR2,SEQ NUMBER)
RETURN VARCHAR2
IS
l_rtn varchar2(1000);
l_pos_start number;
l_pos_end number;
l_count number;
BEGIN
  l_rtn := '';
    l_count := GET_COLCOUNT(COLS,SPLIT_CHAR);
    if seq < 1 or seq > l_count then
      return '';
    end if;
    
    if SEQ = 1 then
      l_pos_start := 1;
    else
      l_pos_start := INSTR(COLS,SPLIT_CHAR,1,SEQ - 1) + 1;
    end if;

  l_pos_end := INSTR(COLS,SPLIT_CHAR,1,SEQ);  
    
    if l_pos_end = 0 then
      l_pos_end := length(cols) + 1;
    end if;
    
    l_rtn := substr(cols,l_pos_start,l_pos_end - l_pos_start);
  RETURN l_rtn;
END;

FUNCTION GET_COLARRAY(COLS VARCHAR2,SPLIT_CHAR VARCHAR2)
RETURN SOURCE_ARRAY
IS
l_array SOURCE_ARRAY;
l_col_count number;
l_col_index number;
BEGIN
  l_array := SOURCE_ARRAY();
  l_col_count := GET_COLCOUNT(COLS,SPLIT_CHAR);
  for l_col_index in 1..l_col_count
  loop
        l_array.EXTEND;    
         l_array(l_array.COUNT) := 
          GET_COLCODE(COLS,SPLIT_CHAR,l_col_index);
  end loop;
    return l_array;
END;
