CREATE TABLE [dbo].[Company](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Exchange] [nvarchar](20) NOT NULL,
	[Ticker] [nvarchar](5) NOT NULL,
	[Isin] [nvarchar](12) NOT NULL,
	[website] [nvarchar](50) NULL
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,
    CONSTRAINT [UQ_code] UNIQUE NONCLUSTERED
    (
        [Isin]
    )
) ON [PRIMARY]

GO

Insert into Company values('Apple Inc.', 'NASDAQ', 'AAPL', 'US0378331005',	'http://www.apple.com');
Insert into Company values('British Airways Plc', 'Pink Sheets', 'BAIRY', 'US1104193065', '');
Insert into Company values('Heineken NV', 'Euronext Amsterdam', 'HEIA', 'NL0000009165', ''); 
Insert into Company values('Panasonic Corp', 'Tokyo Stock Exchange', '6752', 'JP3866800000', 'http://www.panasonic.co.jp');
Insert into Company values('Porsche Automobil',	'Deutsche Börse', 'PAH3', 'DE000PAH0038', 'https://www.porsche.com/');

select * from Company
