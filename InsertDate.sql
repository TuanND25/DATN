insert into AspNetUsers values('e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf',N'Nguyễn Văn Thắng',1,'thangnv','','',1,'','','thang@gmail.com','',1,'','','','0367180646',1,1,'',1,1)
--insert into AspNetUsers values('dcb1b07c-761a-4c2d-8747-b40669fe3e40',N'Nguyễn Duy Tuấn',1,N'tuannd','','',1,'','','tuannd1@gmail.com','',1,'','','','0397180646',1,1,'',1,1)
--insert into AspNetUsers values('55db2d1e-f5b6-454a-8ee1-c56b8731a00a',N'Trần Duy Thái',1,N'client','','',1,'','','thaitd@gmail.com','',1,'','','','0367100646',1,1,'',1,1)

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

insert into ConsumerPoints values('e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf',5000,1)

insert into Formulas values('97b38371-46ee-407b-b802-9072f53d5805',10,1)
insert into Formulas values('aaffb5d6-02fb-4f83-9056-523e2f4e4fc4',5,1)

insert into HistoryConsumerPoints values('193b76c8-4b69-4663-adac-98e5de5225fc','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','97b38371-46ee-407b-b802-9072f53d5805',1)
insert into HistoryConsumerPoints values('f3477e0e-6c6c-4574-a77e-e99530ba4fb5','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','aaffb5d6-02fb-4f83-9056-523e2f4e4fc4',1)
insert into HistoryConsumerPoints values('f170df48-4a56-48e8-9095-500cd4a562a3','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','aaffb5d6-02fb-4f83-9056-523e2f4e4fc4',1)

insert into Bills values('b735bab0-d9cb-40f7-8299-5de01854fcf1','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','2023-10-1',300000,23000,'N123','','f3477e0e-6c6c-4574-a77e-e99530ba4fb5',1)
insert into Bills values('c7048294-a325-4926-993c-1e188e99a717','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','2023-10-1',600000,23000,'MD34','','193b76c8-4b69-4663-adac-98e5de5225fc',1)
insert into Bills values('c7048294-a325-4926-993c-1e188e99a717','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','2023-10-1',900000,23000,'H845','','f170df48-4a56-48e8-9095-500cd4a562a3',1)

insert into Carts values('e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf',N'',1)

insert into Promotions values('26b35f5d-3604-4011-945b-353bc21d8ed0','KM1','KMT10','50',50,'2023-10-4','2023-12-30',N'',N'',1)
insert into Promotions values('e539888f-bd53-44e2-afb1-08f4f448edb5','KM2','THANHVIENMOI','50',50,'2023-10-4','2024-12-30',N'',N'',1)

insert into Products values('a3ce510a-074a-4a51-b590-1bd0eeaa064d',N'Áo thun Outerity Meowment Tee','de92d4b4-ba16-43d5-b7c3-fad74c97fe22',1)
insert into Products values('3b838f25-291c-410d-8dac-05e7b79afcb1',N'Áo Polo Outerity Polo Meowment','b0349aa9-3622-445c-9b43-ddba60038279',1)

insert into Vouchers values('e3855830-78e1-4e84-8341-9580fc938909','Voucher1','HN123',10,100,'2023-9-23','2024-1-1','',1)
insert into Vouchers values('0e79b6cf-cba4-47cc-8736-48c1bd6b8f8c','Voucher2','BTL123',10,100,'2023-9-23','2024-1-1','',1)

insert into BillItems values('')

insert into Reviews values('b90fd1d9-c216-4c48-aeb0-e3e7dd64d86e','e4ff175e-91ab-41ba-aa46-ba4e87fdb5bf','a3ce510a-074a-4a51-b590-1bd0eeaa064d',5,N'ổn','2023-10-1','',1,'')



		public DbSet<BillItems> BillItems { get; set; }

		public DbSet<CartItems> CartItems { get; set; }




		public DbSet<ProductItems> ProductItems { get; set; }

		public DbSet<Reviews> Reviews { get; set; }

		public DbSet<PromotionsProduct> PromotionsProducts { get; set; }



		public DbSet<VoucherBill> VoucherBills { get; set; }