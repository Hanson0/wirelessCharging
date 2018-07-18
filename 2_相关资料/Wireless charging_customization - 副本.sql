-- 数据库初始化脚本
DROP TABLE IF EXISTS `sn`;
CREATE TABLE `sn` (
  `id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `sn` VARCHAR(32) NOT NULL  COMMENT '序列号',
  
  `iin_50`  VARCHAR(8)   COMMENT '50mA输入电流',
  `vout_50` VARCHAR(8)   COMMENT '50mA输出电压',  
  `f_50`   VARCHAR(8)  	 COMMENT '50mA频率',   
  `eff_50` VARCHAR(8)    COMMENT '50mA转换效率',    
  
  `iin_100` VARCHAR(8)   COMMENT '100mA输入电流',
  `vout_100` VARCHAR(8)  COMMENT '100mA输出电压',  
  `f_100` VARCHAR(8)  	 COMMENT '100mA频率',   
  `eff_100` VARCHAR(8)   COMMENT '100mA转换效率',   
  
  `iin_150` VARCHAR(8)   COMMENT '150mA输入电流',
  `vout_150` VARCHAR(8)  COMMENT '150mA输出电压',  
  `f_150` VARCHAR(8)  	 COMMENT '150mA频率',   
  `eff_150` VARCHAR(8)   COMMENT '150mA转换效率',  
 
  `iin_200` VARCHAR(8)   COMMENT '200mA输入电流',
  `vout_200` VARCHAR(8)  COMMENT '200mA输出电压',  
  `f_200` VARCHAR(8)  	 COMMENT '200mA频率',   
  `eff_200` VARCHAR(8)   COMMENT '200mA转换效率',   
  
  `iin_300` VARCHAR(8)   COMMENT '300mA输入电流',
  `vout_300` VARCHAR(8)  COMMENT '300mA输出电压',  
  `f_300` VARCHAR(8)  	 COMMENT '300mA频率',   
  `eff_300` VARCHAR(8)   COMMENT '300mA转换效率',    
  
  `iin_400` VARCHAR(8)   COMMENT '400mA输入电流',
  `vout_400` VARCHAR(8)  COMMENT '400mA输出电压',  
  `f_400` VARCHAR(8)  	 COMMENT '400mA频率',   
  `eff_400` VARCHAR(8)   COMMENT '400mA转换效率',
  
  `iin_500` VARCHAR(8)   COMMENT '500mA输入电流',
  `vout_500` VARCHAR(8)  COMMENT '500mA输出电压',  
  `f_500` VARCHAR(8)  	 COMMENT '500mA频率',   
  `eff_500` VARCHAR(8)   COMMENT '500mA转换效率',  
  
  `iin_600` VARCHAR(8)   COMMENT '600mA输入电流',
  `vout_600` VARCHAR(8)  COMMENT '600mA输出电压',  
  `f_600` VARCHAR(8)  	 COMMENT '600mA频率',   
  `eff_600` VARCHAR(8)   COMMENT '600mA转换效率',    
  `result` VARCHAR(8) NOT NULL COMMENT '测试结果',   
  
  -- `order` VARCHAR(32) NOT NULL COMMENT '订单号',
  `package` VARCHAR(32) COMMENT '箱号',
  `create_time` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `update_time` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`),
  KEY `index_sn` (`sn`),     
  -- KEY `index_order` (`order`),
  KEY `index_package` (`package`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COMMENT='测试记录表';


--性能测试：
-- 查询SN的是否性能测试已通过，如果为PASS则报警已经测试通过，否则就开始测试并记录数据和结果。
SELECT `result` FROM sn WHERE `sn` = 'WPC-W-A-RX-CF-0122018042700002' and `result` = 'pass';
--判断result列表有无
--开始测试
-- 记录数据
--插入所选模式固定的各项测试条件的测试参数，和测试结果
INSERT INTO sn (`sn`, `iin_50`,`vout_50`,`f_50`,`eff_50`,`iin_100`,`vout_100`,`f_100`, `eff_100`, `iin_150`, `vout_150`,`f_150`,`eff_150`,`iin_200`,`vout_200`,`f_200`,`eff_200`,
`iin_300`,`vout_300`,`f_300`,`eff_300`,`iin_400`,`vout_400`,`f_400`,`eff_400`,`iin_500`,`vout_500`,`f_500`,`eff_500`,`iin_600`,`vout_600`,`f_600`,`eff_600`,`result`,`create_time`)
VALUES ('WPC-W-A-RX-CF-0122018042700002', '600', '4.85', '128','0.8','610', '610', '610', '610','620', '620', '620', '620', '630', '630', '630', '630', '640', '640', '640', '640',
 '', '', '', '','660', '660', '660', '660','670', '670', '670', '670','pass','2018-05-25 16:04:32'); --设置32个变量，默认值为""，若测试项有值,则赋值,再插入一行

 
 
 -- 包装：
 -- 查询SN号,看性能测试是否过站。如果没有查询到，则报警性能测试没有过站;否则，过站下一步。
 SELECT * FROM sn WHERE `sn` = 'WPC-W-A-RX-CF-0122018042700002' and `result` = 'pass';
 -- 查询SN号,看绑有箱号。如果package不为空，则报警SN...已经绑有箱号：...，否则下一步（查询输入的箱号是否绑满）
 SELECT `package` FROM sn WHERE `sn` = 'WPC-W-A-RX-CF-0122018042700002' and `result` = 'pass';
 --查询指定箱号中的sn数量是否等于客户端设置的这箱数量,等于则说明已经装满,否则进行下一步
 SELECT `sn` FROM sn WHERE `package` = 'pack1';--箱号为pack1的SN数量
 --更新箱号
UPDATE sn SET `package`='pack1' WHERE `sn` = 'WPC-W-A-RX-CF-0122018042700002'and `result` = 'pass';


