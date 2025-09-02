-- 检查USERS表的实际结构
SELECT column_name, data_type, data_length, nullable, data_default
FROM user_tab_columns 
WHERE table_name = 'USERS'
ORDER BY column_id;

-- 检查USERS表中的数据样本
SELECT * FROM USERS WHERE ROWNUM <= 5;

-- 检查是否有任何字段可能被错误映射
SELECT column_name, data_type 
FROM user_tab_columns 
WHERE table_name = 'USERS' 
AND data_type IN ('CHAR', 'VARCHAR2', 'NUMBER', 'DATE', 'TIMESTAMP', 'INTERVAL')
ORDER BY column_id;
