-- =============================================
-- 检查并修复COUPON_MANAGERS主键序列问题
-- =============================================

-- 1. 检查是否存在序列
SELECT 
    sequence_name AS "序列名称",
    min_value AS "最小值",
    max_value AS "最大值",
    increment_by AS "增量",
    last_number AS "最后值",
    cache_size AS "缓存大小"
FROM USER_SEQUENCES 
WHERE sequence_name LIKE '%COUPON%' OR sequence_name LIKE '%MANAGER%';

-- 2. 检查表的主键约束
SELECT 
    uc.CONSTRAINT_NAME AS "主键约束名称",
    uc.CONSTRAINT_TYPE AS "约束类型",
    ucc.COLUMN_NAME AS "主键列名"
FROM USER_CONSTRAINTS uc
JOIN USER_CONS_COLUMNS ucc ON uc.CONSTRAINT_NAME = ucc.CONSTRAINT_NAME
WHERE uc.TABLE_NAME = 'COUPON_MANAGERS' 
    AND uc.CONSTRAINT_TYPE = 'P';

-- 3. 检查当前最大ID
SELECT NVL(MAX(COUPONMANAGERID), 0) AS "当前最大ID" FROM COUPON_MANAGERS;

-- 4. 创建序列（如果不存在）
-- 注意：Oracle序列名称不能超过30个字符
CREATE SEQUENCE SEQ_COUPON_MANAGERS
    START WITH 1
    INCREMENT BY 1
    NOCACHE
    NOCYCLE;

-- 5. 创建触发器来自动设置主键（如果不存在）
CREATE OR REPLACE TRIGGER TRG_COUPON_MANAGERS_ID
    BEFORE INSERT ON COUPON_MANAGERS
    FOR EACH ROW
BEGIN
    IF :NEW.COUPONMANAGERID IS NULL THEN
        SELECT SEQ_COUPON_MANAGERS.NEXTVAL INTO :NEW.COUPONMANAGERID FROM DUAL;
    END IF;
END;
/

-- 6. 验证序列和触发器
SELECT 
    '序列SEQ_COUPON_MANAGERS' AS "对象类型",
    sequence_name AS "对象名称",
    last_number AS "当前值"
FROM USER_SEQUENCES 
WHERE sequence_name = 'SEQ_COUPON_MANAGERS';

SELECT 
    '触发器TRG_COUPON_MANAGERS_ID' AS "对象类型",
    trigger_name AS "对象名称",
    status AS "状态"
FROM USER_TRIGGERS 
WHERE trigger_name = 'TRG_COUPON_MANAGERS_ID';

-- 7. 测试插入（可选）
-- INSERT INTO COUPON_MANAGERS (
--     COUPONNAME, COUPONTYPE, MINIMUMSPEND, DISCOUNTAMOUNT, DISCOUNTRATE, 
--     TOTALQUANTITY, USEDQUANTITY, VALIDFROM, VALIDTO, DESCRIPTION, 
--     STOREID, SELLERID
-- ) VALUES (
--     '测试序列券', 0, 50.00, 10.00, NULL,
--     100, 0, TIMESTAMP '2024-01-01 00:00:00', TIMESTAMP '2024-12-31 23:59:59',
--     '测试序列自动生成ID', 101, 3
-- );
-- COMMIT;
