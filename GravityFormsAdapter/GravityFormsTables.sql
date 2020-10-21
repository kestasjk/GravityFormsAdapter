
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblGravityEntries](
	[id] [int] NOT NULL,
	[form_id] [int] NOT NULL,
	[created_by] [int] NOT NULL,
	[date_created] [nvarchar](max) NOT NULL,
	[is_starred] [bit] NOT NULL,
	[is_read] [bit] NOT NULL,
	[ip] [nvarchar](max) NULL,
	[source_url] [nvarchar](max) NULL,
	[post_id] [int] NULL,
	[user_agent] [nvarchar](max) NULL,
	[status] [nvarchar](max) NULL,
	[currency] [nvarchar](max) NULL,
	[payment_status] [nvarchar](max) NULL,
	[payment_date] [nvarchar](max) NULL,
	[payment_amount] [decimal](18, 3) NULL,
	[transaction_id] [nvarchar](max) NULL,
	[is_fulfilled] [bit] NULL,
	[transaction_type] [int] NULL,
 CONSTRAINT [PK_tblGravityFormEntries] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE TABLE [dbo].[tblGravityEntryValues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[entry_id] [int] NOT NULL,
	[entryKey] [nvarchar](max) NULL,
	[entrySubKey] [nvarchar](max) NULL,
	[entryValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblGravityFormsEntryValues] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE TABLE [dbo].[tblGravityFieldInputs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[field_id] [int] NOT NULL,
	[label] [nvarchar](max) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_tblGravityFieldInputs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE TABLE [dbo].[tblGravityFields](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[form_id] [int] NOT NULL,
	[field_id] [nvarchar](max) NOT NULL,
	[type] [nvarchar](max) NOT NULL,
	[label] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_tblGravityFields] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE TABLE [dbo].[tblGravityForms](
	[id] [int] NOT NULL,
	[title] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblGravityForms] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

