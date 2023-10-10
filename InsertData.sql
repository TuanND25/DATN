insert into AspNetUsers values('e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf',N'Nguyễn Văn Thắng',1,'thangnv','','',1,'','','thang@gmail.com','',1,'','','','0367180646',1,1,'',1,1)
insert into AspNetUsers values('dcb1b07c-761a-4c2d-8747-b40669fe3e40',N'Nguyễn Duy Tuấn',1,N'tuannd','','',1,'','','tuannd1@gmail.com','',1,'','','','0397180646',1,1,'',1,1)
insert into AspNetUsers values('55db2d1e-f5b6-454a-8ee1-c56b8731a00a',N'Trần Duy Thái',1,N'client','','',1,'','','thaitd@gmail.com','',1,'','','','0367100646',1,1,'',1,1)

insert into AddressShips values('cbdd7ca2-0a2b-47c8-96aa-55abf1334776','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','','','','',N'Số nhà 22 ngõ 132 cầu diễn','0932459037',1)
insert into AddressShips values('5acc1e85-72ff-4641-b73e-753642a6055b','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','','','','',N'sn 24 ngõ 153 Phú Diễn','0932459037',2)

insert into Colors values('5bc1a17e-a2c1-42bf-8de5-31cf2a0ee53a',N'Trắng',1)
insert into Colors values('79b80749-b18b-4875-9f27-75f5d6ad4f93',N'Đen',1)
insert into Colors values('7f697aeb-42fd-4c32-bc13-ed11f88ab74a',N'Xám',1)
insert into Colors values('0ed1e297-5644-4bc7-9486-cf69bf7994a2',N'Kem',1)

insert into Sizes values('d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','S',1)
insert into Sizes values('6778ef5b-5901-46e2-9ca9-7bea36e4ff9b','M',1)
insert into Sizes values('c1377708-db38-440b-833d-0e99259e50bb','L',1)
insert into Sizes values('70591428-7358-4c0d-a7b7-4a30f1236f15','XL',1)


insert into Categories values('de92d4b4-ba16-43d5-b7c3-fad74c97fe22',N'Áo thun',1)
insert into Categories values('d6d95297-754f-4428-9504-21cf9a390eb5',N'Áo Hoodie',1)
insert into Categories values('a6b68fb7-e600-46db-b15f-2fe4e09b72a4',N'Áo Sweater',1)
insert into Categories values('b0349aa9-3622-445c-9b43-ddba60038279',N'Áo Polo',1)

insert into PaymentMethods values('261d402e-dfa8-4213-9e29-4fdd6fc6b95d',N'Tiền mặt',1)
insert into PaymentMethods values('1dcb551d-9938-468d-9057-ed3c66cf5f9d',N'Chuyển khoản',1)
insert into PaymentMethods values('290bd269-c5c7-47a3-8e2e-80f610b6d06d',N'Chuyển khoản và tiền mặt',1)

insert into ConsumerPoints values('e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf',5000,1)

insert into Formulas values('97b38371-46ee-407b-b802-9072f53d5805',10,1)
insert into Formulas values('aaffb5d6-02fb-4f83-9056-523e2f4e4fc4',5,1)

insert into HistoryConsumerPoints values('193b76c8-4b69-4663-adac-98e5de5225fc','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','97b38371-46ee-407b-b802-9072f53d5805',1)
insert into HistoryConsumerPoints values('f3477e0e-6c6c-4574-a77e-e99530ba4fb5','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','aaffb5d6-02fb-4f83-9056-523e2f4e4fc4',1)
insert into HistoryConsumerPoints values('f170df48-4a56-48e8-9095-500cd4a562a3','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','aaffb5d6-02fb-4f83-9056-523e2f4e4fc4',1)

insert into Vouchers values('e3855830-78e1-4e84-8341-9580fc938909','Voucher1','HN123',10,100,'2023-9-23','2024-1-1','',1)
insert into Vouchers values('0e79b6cf-cba4-47cc-8736-48c1bd6b8f8c','Voucher2','BTL123',10,100,'2023-9-23','2024-1-1','',1)

insert into Bills values('b735bab0-d9cb-40f7-8299-5de01854fcf1','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','193b76c8-4b69-4663-adac-98e5de5225fc','261d402e-dfa8-4213-9e29-4fdd6fc6b95d','e3855830-78e1-4e84-8341-9580fc938909','HD01',300000,10000,290000,0,290000,'2023-9-20','2023-9-22','2023-9-23',N'Bán tại quầy',null,1)
insert into Bills values('b76c143a-c3fe-4456-940c-7135cd925c7d','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','f3477e0e-6c6c-4574-a77e-e99530ba4fb5','261d402e-dfa8-4213-9e29-4fdd6fc6b95d',null,'HD02',300000,0,300000,0,300000,'2023-9-20','2023-9-22','2023-9-23',N'Bán tại quầy',null,1)
insert into Bills values('306b1e4d-8b6f-48f3-b3eb-956b524a3ef9','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','f170df48-4a56-48e8-9095-500cd4a562a3','261d402e-dfa8-4213-9e29-4fdd6fc6b95d',null,'HD03',250000,0,250000,0,250000,'2023-9-20','2023-9-22','2023-9-23',N'Bán tại quầy',null,1)


insert into Carts values('e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf',N'',1)

insert into Promotions values('26b35f5d-3604-4011-945b-353bc21d8ed0','KM1','KMT10','50',50,'2023-10-4','2023-12-30',N'',N'',1)
insert into Promotions values('e539888f-bd53-44e2-afb1-08f4f448edb5','KM2','THANHVIENMOI','50',50,'2023-10-4','2024-12-30',N'',N'',1)

insert into Products values('a3ce510a-074a-4a51-b590-1bd0eeaa064d',N'Áo thun Outerity Meowment Tee',1)
insert into Products values('3b838f25-291c-410d-8dac-05e7b79afcb1',N'Áo Polo Outerity Polo Meowment',1)


insert into ProductItems values('adf8e940-28df-47e1-8bf2-4e7ec92ec784','a3ce510a-074a-4a51-b590-1bd0eeaa064d','5bc1a17e-a2c1-42bf-8de5-31cf2a0ee53a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)	
insert into ProductItems values('6880b307-d5da-4c93-b74b-73bcad8702e9','a3ce510a-074a-4a51-b590-1bd0eeaa064d','79b80749-b18b-4875-9f27-75f5d6ad4f93','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)	
insert into ProductItems values('dd634415-4758-4e01-9401-20e148681866','a3ce510a-074a-4a51-b590-1bd0eeaa064d','7f697aeb-42fd-4c32-bc13-ed11f88ab74a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)	
insert into ProductItems values('96876713-8c3a-4db1-a72c-5220c715b81b','a3ce510a-074a-4a51-b590-1bd0eeaa064d','0ed1e297-5644-4bc7-9486-cf69bf7994a2','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)
	

insert into ProductItems values('48d91834-0b5f-45f0-9202-206e47c55759','a3ce510a-074a-4a51-b590-1bd0eeaa064d','5bc1a17e-a2c1-42bf-8de5-31cf2a0ee53a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)	
insert into ProductItems values('218daa80-ee92-4ffb-85c8-0ed02f99367f','a3ce510a-074a-4a51-b590-1bd0eeaa064d','79b80749-b18b-4875-9f27-75f5d6ad4f93','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)	
insert into ProductItems values('9a1a7cc2-455c-425d-acba-b0ce54fdb892','a3ce510a-074a-4a51-b590-1bd0eeaa064d','7f697aeb-42fd-4c32-bc13-ed11f88ab74a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)	
insert into ProductItems values('a2f8f40e-e3eb-41e8-b1cc-35b3b0c6bbec','a3ce510a-074a-4a51-b590-1bd0eeaa064d','0ed1e297-5644-4bc7-9486-cf69bf7994a2','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)


insert into ProductItems values('9210148d-9ffc-4198-87c9-7f37bc97de45','a3ce510a-074a-4a51-b590-1bd0eeaa064d','5bc1a17e-a2c1-42bf-8de5-31cf2a0ee53a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)	
insert into ProductItems values('1b7bf899-689d-4dbb-9b14-d383b75214a2','a3ce510a-074a-4a51-b590-1bd0eeaa064d','79b80749-b18b-4875-9f27-75f5d6ad4f93','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)	
insert into ProductItems values('a9137841-c184-41b0-bb79-e81ecdcd861e','a3ce510a-074a-4a51-b590-1bd0eeaa064d','7f697aeb-42fd-4c32-bc13-ed11f88ab74a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)	
insert into ProductItems values('9b4f1171-50e2-4684-baba-d4975e69e2f6','a3ce510a-074a-4a51-b590-1bd0eeaa064d','0ed1e297-5644-4bc7-9486-cf69bf7994a2','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)


insert into ProductItems values('de35f925-bb1f-4de7-9146-2b61dba050a3','a3ce510a-074a-4a51-b590-1bd0eeaa064d','5bc1a17e-a2c1-42bf-8de5-31cf2a0ee53a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)	
insert into ProductItems values('71efdb82-1d37-4bd0-96c0-2cf2f857e25d','a3ce510a-074a-4a51-b590-1bd0eeaa064d','79b80749-b18b-4875-9f27-75f5d6ad4f93','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)	
insert into ProductItems values('e8dbb0cd-c7c1-4b83-a725-5d5bbecb8132','a3ce510a-074a-4a51-b590-1bd0eeaa064d','7f697aeb-42fd-4c32-bc13-ed11f88ab74a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)	
insert into ProductItems values('d9079060-cfe1-4531-9af1-38db9b43e13b','a3ce510a-074a-4a51-b590-1bd0eeaa064d','0ed1e297-5644-4bc7-9486-cf69bf7994a2','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1000,150000,250000,1)



insert into ProductItems values('f1a2ddaf-9905-48c7-82d9-f4f8f02d43b1','3b838f25-291c-410d-8dac-05e7b79afcb1','5bc1a17e-a2c1-42bf-8de5-31cf2a0ee53a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)	
insert into ProductItems values('59ca3315-9ed2-43de-a0ab-74f5300df43c','3b838f25-291c-410d-8dac-05e7b79afcb1','79b80749-b18b-4875-9f27-75f5d6ad4f93','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)	
insert into ProductItems values('01aad59c-9e6e-43bf-bcaf-b0998b32ed25','3b838f25-291c-410d-8dac-05e7b79afcb1','7f697aeb-42fd-4c32-bc13-ed11f88ab74a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)	
insert into ProductItems values('2b51c437-0584-400c-8b1b-ee0690e330dd','3b838f25-291c-410d-8dac-05e7b79afcb1','0ed1e297-5644-4bc7-9486-cf69bf7994a2','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)
	

insert into ProductItems values('953560d4-719c-45e6-b14c-cd6b61d5d65b','3b838f25-291c-410d-8dac-05e7b79afcb1','5bc1a17e-a2c1-42bf-8de5-31cf2a0ee53a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)	
insert into ProductItems values('56d26e67-36ca-4f0f-b411-e96573d76b44','3b838f25-291c-410d-8dac-05e7b79afcb1','79b80749-b18b-4875-9f27-75f5d6ad4f93','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)	
insert into ProductItems values('fba5eca5-52b1-4bf6-a0d5-e9fdca27d800','3b838f25-291c-410d-8dac-05e7b79afcb1','7f697aeb-42fd-4c32-bc13-ed11f88ab74a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)	
insert into ProductItems values('f732ccfa-3dd2-49e1-a522-c179f1589c44','3b838f25-291c-410d-8dac-05e7b79afcb1','0ed1e297-5644-4bc7-9486-cf69bf7994a2','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)


insert into ProductItems values('6ca953a7-1d2e-40d6-b6c9-ca3223ae7b41','3b838f25-291c-410d-8dac-05e7b79afcb1','5bc1a17e-a2c1-42bf-8de5-31cf2a0ee53a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)	
insert into ProductItems values('b6fea6c5-fea1-49c7-80bb-4e703c91a905','3b838f25-291c-410d-8dac-05e7b79afcb1','79b80749-b18b-4875-9f27-75f5d6ad4f93','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)	
insert into ProductItems values('2a447516-9558-47b8-98b1-a9ad3ba4a2aa','3b838f25-291c-410d-8dac-05e7b79afcb1','7f697aeb-42fd-4c32-bc13-ed11f88ab74a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)	
insert into ProductItems values('cde3febb-aa0e-4051-9b7f-3cc408dac9f9','3b838f25-291c-410d-8dac-05e7b79afcb1','0ed1e297-5644-4bc7-9486-cf69bf7994a2','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)


insert into ProductItems values('bae06b8e-457c-4404-9245-e78624917619','3b838f25-291c-410d-8dac-05e7b79afcb1','5bc1a17e-a2c1-42bf-8de5-31cf2a0ee53a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)	
insert into ProductItems values('68fb5967-c69d-4777-bb3b-840da440e852','3b838f25-291c-410d-8dac-05e7b79afcb1','79b80749-b18b-4875-9f27-75f5d6ad4f93','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)	
insert into ProductItems values('77f4be97-62df-4c5b-a9b1-7f690a1db3e1','3b838f25-291c-410d-8dac-05e7b79afcb1','7f697aeb-42fd-4c32-bc13-ed11f88ab74a','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)	
insert into ProductItems values('dd5a54ef-375a-41ed-b439-10b9a4b28d7e','3b838f25-291c-410d-8dac-05e7b79afcb1','0ed1e297-5644-4bc7-9486-cf69bf7994a2','d1e0eec9-81a1-4dd8-a70f-469f1d0fe021','b0349aa9-3622-445c-9b43-ddba60038279',1000,150000,250000,1)



insert into BillItems values('c32434ee-d576-4825-b95a-bd1980a7f497','b735bab0-d9cb-40f7-8299-5de01854fcf1','953560d4-719c-45e6-b14c-cd6b61d5d65b',1,300000,1)
insert into BillItems values('24762267-9bfe-4809-96fa-61469ddc156a','b76c143a-c3fe-4456-940c-7135cd925c7d','b6fea6c5-fea1-49c7-80bb-4e703c91a905',1,300000,1)
insert into BillItems values('f39a16e4-e9e5-45ea-9edf-c061ad92c58a','306b1e4d-8b6f-48f3-b3eb-956b524a3ef9','48d91834-0b5f-45f0-9202-206e47c55759',1,250000,1)


insert into CartItems values('daf116e9-f0d3-49fe-acac-1decac254547','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','953560d4-719c-45e6-b14c-cd6b61d5d65b',1,300000,1)
insert into CartItems values('ee64ac1e-4944-4e88-ad45-ee0ce49718be','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','b6fea6c5-fea1-49c7-80bb-4e703c91a905',1,300000,1)
insert into CartItems values('bbf3ca4e-690e-48ae-a66d-275e2a311dde','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','48d91834-0b5f-45f0-9202-206e47c55759',1,250000,1)

insert into Reviews values('b90fd1d9-c216-4c48-aeb0-e3e7dd64d86e','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf',5,N'ổn','2023-10-1','',1,'c32434ee-d576-4825-b95a-bd1980a7f497')

insert into PromotionsProducts values('ddaebab1-0474-48d6-b883-fc4d16ac3d6c','26b35f5d-3604-4011-945b-353bc21d8ed0','6ca953a7-1d2e-40d6-b6c9-ca3223ae7b41',1)
insert into PromotionsProducts values('946715b1-718b-4edf-b422-b80de7b13baa','26b35f5d-3604-4011-945b-353bc21d8ed0','de35f925-bb1f-4de7-9146-2b61dba050a3',1)
insert into PromotionsProducts values('10a699c4-4914-4eb5-8cd8-e4f057cfbfd3','e539888f-bd53-44e2-afb1-08f4f448edb5','dd5a54ef-375a-41ed-b439-10b9a4b28d7e',1)

insert into VoucherUsers values('cd9ca93b-9432-46d1-a117-61f2fc785e01','e3855830-78e1-4e84-8341-9580fc938909','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf',1)
insert into VoucherUsers values('de63c74f-586a-4d32-b883-3e9cf6374e85','0e79b6cf-cba4-47cc-8736-48c1bd6b8f8c','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf',1)

