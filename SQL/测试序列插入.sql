-- =============================================
-- 测试序列插入功能
-- =============================================

-- 1. 检查当前序列状态
SELECT 
    sequence_name AS "序列名称",
    last_number AS "当前值"
FROM USER_SEQUENCES 
WHERE sequence_name = 'SEQ_COUPON_MANAGERS';

-- 2. 检查当前表中的最大ID
SELECT NVL(MAX(COUPONMANAGERID), 0) AS "当前最大ID" FROM COUPON_MANAGERS;

-- 3. 测试插入（不指定主键，让序列自动生成）
INSERT INTO COUPON_MANAGERS (
    COUPONNAME, COUPONTYPE, MINIMUMSPEND, DISCOUNTAMOUNT, DISCOUNTRATE, 
    TOTALQUANTITY, USEDQUANTITY, VALIDFROM, VALIDTO, DESCRIPTION, 
    STOREID, SELLERID
) VALUES (
    '测试序列券', 0, 50.00, 10.00, NULL,
    100, 0, TIMESTAMP '2024-01-01 00:00:00', TIMESTAMP '2024-12-31 23:59:59',
    '测试序列自动生成ID', 101, 3
);

-- 4. 提交事务
COMMIT;

-- 5. 验证插入结果
SELECT 
    COUPONMANAGERID AS "优惠券ID",
    COUPONNAME AS "优惠券名称",
    SELLERID AS "商家ID"
FROM COUPON_MANAGERS 
WHERE COUPONNAME = '测试序列券'
ORDER BY COUPONMANAGERID;

-- 6. 再次检查序列状态
SELECT 
    sequence_name AS "序列名称",
    last_number AS "当前值"
FROM USER_SEQUENCES 
WHERE sequence_name = 'SEQ_COUPON_MANAGERS';
