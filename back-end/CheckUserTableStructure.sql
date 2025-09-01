-- 检查USERS表结构的SQL查询
-- 用于确认数据库中USERS表的实际字段

-- 1. 查看USERS表的所有列信息
SELECT 
    COLUMN_NAME as "列名",
    DATA_TYPE as "数据类型",
    DATA_LENGTH as "数据长度",
    DATA_PRECISION as "精度",
    DATA_SCALE as "小数位",
    NULLABLE as "可空",
    DATA_DEFAULT as "默认值"
FROM USER_TAB_COLUMNS 
WHERE TABLE_NAME = 'USERS'
ORDER BY COLUMN_ID;

-- 2. 检查是否存在AdministratorUserID列
SELECT 
    CASE 
        WHEN COUNT(*) > 0 THEN '存在AdministratorUserID列'
        ELSE '不存在AdministratorUserID列'
    END as "AdministratorUserID列状态"
FROM USER_TAB_COLUMNS 
WHERE TABLE_NAME = 'USERS' 
AND COLUMN_NAME = 'AdministratorUserID';

-- 3. 查看USERS表的所有约束
SELECT 
    CONSTRAINT_NAME as "约束名",
    CONSTRAINT_TYPE as "约束类型",
    SEARCH_CONDITION as "检查条件"
FROM USER_CONSTRAINTS 
WHERE TABLE_NAME = 'USERS';

-- 4. 查看USERS表的外键约束
SELECT 
    A.CONSTRAINT_NAME as "外键约束名",
    A.COLUMN_NAME as "外键列名",
    B.TABLE_NAME as "引用表名",
    B.COLUMN_NAME as "引用列名"
FROM USER_CONS_COLUMNS A
JOIN USER_CONS_COLUMNS B ON A.CONSTRAINT_NAME = B.CONSTRAINT_NAME
JOIN USER_CONSTRAINTS C ON A.CONSTRAINT_NAME = C.CONSTRAINT_NAME
WHERE C.TABLE_NAME = 'USERS' 
AND C.CONSTRAINT_TYPE = 'R'
AND A.POSITION = B.POSITION;

-- 5. 查看USERS表的索引
SELECT 
    INDEX_NAME as "索引名",
    COLUMN_NAME as "列名",
    COLUMN_POSITION as "位置"
FROM USER_IND_COLUMNS 
WHERE TABLE_NAME = 'USERS'
ORDER BY INDEX_NAME, COLUMN_POSITION;

-- 6. 检查USERS表的行数
SELECT COUNT(*) as "USERS表行数" FROM USERS;

-- 7. 查看USERS表的前5行数据（仅显示关键字段）
SELECT 
    USERID,
    USERNAME,
    EMAIL,
    ROLE,
    ACCOUNTCREATIONTIME
FROM USERS 
WHERE ROWNUM <= 5;

-- 8. 如果存在AdministratorUserID列，查看其数据分布
SELECT 
    AdministratorUserID,
    COUNT(*) as "记录数"
FROM USERS 
WHERE AdministratorUserID IS NOT NULL
GROUP BY AdministratorUserID
ORDER BY AdministratorUserID;
