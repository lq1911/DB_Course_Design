-- 检查STORES表的详细结构
-- 1. 检查表结构
SELECT column_name, data_type, data_length, nullable, data_default, column_id
FROM user_tab_columns 
WHERE table_name = 'STORES'
ORDER BY column_id;

-- 2. 检查约束信息
SELECT constraint_name, constraint_type, search_condition
FROM user_constraints 
WHERE table_name = 'STORES';

-- 3. 检查外键约束
SELECT a.constraint_name, a.table_name, a.column_name, 
       c_pk.table_name AS foreign_table_name,
       c_pk.constraint_name AS foreign_constraint_name
FROM user_cons_columns a
JOIN user_constraints c ON a.owner = c.owner 
                      AND a.constraint_name = c.constraint_name
JOIN user_constraints c_pk ON c.r_owner = c_pk.owner 
                           AND c.r_constraint_name = c_pk.constraint_name
WHERE c.constraint_type = 'R' 
  AND a.table_name = 'STORES';

-- 4. 检查索引
SELECT index_name, index_type, uniqueness
FROM user_indexes 
WHERE table_name = 'STORES';

-- 5. 检查是否有数据
SELECT COUNT(*) as total_records FROM STORES;

-- 6. 检查SELLERID字段的具体情况
SELECT column_name, data_type, data_length, nullable
FROM user_tab_columns 
WHERE table_name = 'STORES' 
  AND column_name LIKE '%SELLER%';
