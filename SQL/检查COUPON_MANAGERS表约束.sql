-- =============================================
-- 检查COUPON_MANAGERS表的约束信息 (Oracle 19c)
-- =============================================

-- 1. 检查表的主键约束
SELECT 
    uc.CONSTRAINT_NAME AS "主键约束名称",
    uc.CONSTRAINT_TYPE AS "约束类型",
    ucc.COLUMN_NAME AS "主键列名",
    uc.TABLE_NAME AS "表名"
FROM USER_CONSTRAINTS uc
JOIN USER_CONS_COLUMNS ucc ON uc.CONSTRAINT_NAME = ucc.CONSTRAINT_NAME
WHERE uc.TABLE_NAME = 'COUPON_MANAGERS' 
    AND uc.CONSTRAINT_TYPE = 'P'
ORDER BY ucc.POSITION;

-- 2. 检查表的所有约束
SELECT 
    uc.CONSTRAINT_NAME AS "约束名称",
    uc.CONSTRAINT_TYPE AS "约束类型",
    uc.STATUS AS "约束状态",
    uc.DEFERRABLE AS "是否可延迟",
    uc.DEFERRED AS "是否延迟",
    uc.VALIDATED AS "是否验证",
    uc.GENERATED AS "是否生成",
    uc.BAD AS "是否错误",
    uc.RELY AS "是否依赖"
FROM USER_CONSTRAINTS uc
WHERE uc.TABLE_NAME = 'COUPON_MANAGERS'
ORDER BY uc.CONSTRAINT_TYPE, uc.CONSTRAINT_NAME;

-- 3. 检查表结构
SELECT 
    utc.COLUMN_NAME AS "列名",
    utc.DATA_TYPE AS "数据类型",
    utc.DATA_LENGTH AS "数据长度",
    utc.DATA_PRECISION AS "精度",
    utc.DATA_SCALE AS "小数位数",
    utc.NULLABLE AS "允许空值",
    utc.DATA_DEFAULT AS "默认值",
    utc.COLUMN_ID AS "列位置"
FROM USER_TAB_COLUMNS utc
WHERE utc.TABLE_NAME = 'COUPON_MANAGERS'
ORDER BY utc.COLUMN_ID;

-- 4. 检查当前表中的数据
SELECT 
    COUPONMANAGERID AS "优惠券ID",
    COUPONNAME AS "优惠券名称",
    COUPONTYPE AS "类型",
    SELLERID AS "商家ID",
    STOREID AS "店铺ID"
FROM COUPON_MANAGERS 
ORDER BY COUPONMANAGERID;

-- 5. 检查主键序列（如果有的话）
SELECT 
    sequence_name AS "序列名称",
    min_value AS "最小值",
    max_value AS "最大值",
    increment_by AS "增量",
    last_number AS "最后值"
FROM USER_SEQUENCES 
WHERE sequence_name LIKE '%COUPON%' OR sequence_name LIKE '%MANAGER%';

-- 6. 检查是否有触发器
SELECT 
    ut.TRIGGER_NAME AS "触发器名称",
    ut.TRIGGER_TYPE AS "触发器类型",
    ut.TRIGGERING_EVENT AS "触发事件",
    ut.STATUS AS "触发器状态"
FROM USER_TRIGGERS ut
WHERE ut.TABLE_NAME = 'COUPON_MANAGERS';
