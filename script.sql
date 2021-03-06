USE [myl]
GO
/****** Object:  Table [dbo].[CLI_DESC_ARTICULO]    Script Date: 28/2/2020 15:29:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLI_DESC_ARTICULO](
	[CODCLI] [int] NOT NULL,
	[CODPIEZA] [varchar](20) NOT NULL,
	[ART_DESCUENTO] [money] NULL,
	[FECHA] [datetime] NULL,
	[USU_NOMBRE] [char](20) NULL,
	[FECHA_ALTA] [datetime] NULL,
	[FECHA_BAJA] [datetime] NULL,
	[USU_NOMBRE_BAJA] [char](20) NULL,
	[DTO_COMPARTIDO] [char](1) NULL,
 CONSTRAINT [PK_CLI_DESC_ARTICULO] PRIMARY KEY NONCLUSTERED 
(
	[CODCLI] ASC,
	[CODPIEZA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CLI_DESC_MARCA]    Script Date: 28/2/2020 15:29:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLI_DESC_MARCA](
	[CODCLI] [int] NOT NULL,
	[MAR_CODIGO] [int] NOT NULL,
	[MAR_DESCUENTO] [money] NULL,
	[FECHA] [datetime] NULL,
	[USU_NOMBRE] [char](20) NULL,
	[FECHA_ALTA] [datetime] NULL,
	[FECHA_BAJA] [datetime] NULL,
	[USU_NOMBRE_BAJA] [char](20) NULL,
	[DTO_COMPARTIDO] [char](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CLI_DESC_RUBRO]    Script Date: 28/2/2020 15:29:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLI_DESC_RUBRO](
	[CODCLI] [int] NOT NULL,
	[RUB_CODIGO] [int] NOT NULL,
	[CLI_DESCUENTO] [money] NOT NULL,
	[ADICIONAL] [float] NULL,
	[FECHA] [datetime] NULL,
	[USU_NOMBRE] [char](20) NULL,
	[FECHA_ALTA] [datetime] NULL,
	[FECHA_BAJA] [datetime] NULL,
	[USU_NOMBRE_BAJA] [char](20) NULL,
	[DTO_COMPARTIDO] [char](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CLI_DESC_TIPO]    Script Date: 28/2/2020 15:29:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLI_DESC_TIPO](
	[CODCLI] [int] NOT NULL,
	[TIP_CODIGO] [int] NOT NULL,
	[TIP_DESCUENTO] [money] NULL,
	[FECHA] [datetime] NULL,
	[USU_NOMBRE] [char](20) NULL,
	[FECHA_ALTA] [datetime] NULL,
	[FECHA_BAJA] [datetime] NULL,
	[USU_NOMBRE_BAJA] [char](20) NULL,
	[DTO_COMPARTIDO] [char](1) NULL,
 CONSTRAINT [PK_CLI_DESC_TIPO] PRIMARY KEY NONCLUSTERED 
(
	[CODCLI] ASC,
	[TIP_CODIGO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[det_pedidos_web]    Script Date: 28/2/2020 15:29:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[det_pedidos_web](
	[id_pedido_web] [int] NOT NULL,
	[codPieza] [nvarchar](20) NOT NULL,
	[cant] [int] NOT NULL,
	[precioUnitario] [decimal](18, 4) NULL,
 CONSTRAINT [PK_det_pedidos_web_id_pedido_web] PRIMARY KEY CLUSTERED 
(
	[id_pedido_web] ASC,
	[codPieza] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[e2_UsuariosWeb]    Script Date: 28/2/2020 15:29:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[e2_UsuariosWeb](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](80) NOT NULL,
	[NombreCompleto] [nvarchar](256) NULL,
	[Password] [nvarchar](256) NOT NULL,
	[Activo] [bit] NOT NULL,
	[IdRol] [int] NOT NULL,
	[FechaCreacionUTC] [datetime] NOT NULL,
	[FechaCambioPasswordUTC] [datetime] NULL,
	[Email] [nvarchar](80) NULL,
	[UltimaActividadUTC] [datetime] NULL,
 CONSTRAINT [PK_UsuariosGlobal] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_UsuariosGlobal] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FLETE]    Script Date: 28/2/2020 15:29:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FLETE](
	[CODFLETE] [smallint] NOT NULL,
	[NOMBRE] [varchar](50) NOT NULL,
	[PRECIO] [money] NOT NULL,
	[DOMICILIO] [varchar](50) NULL,
	[TELEFONO] [varchar](100) NULL,
	[OBSERVACION] [varchar](254) NULL,
	[DESTINOS] [varchar](254) NULL,
	[CUIT] [char](13) NULL,
	[DGR] [char](13) NULL,
	[LUNES] [char](1) NULL,
	[MARTES] [char](1) NULL,
	[MIERCOLES] [char](1) NULL,
	[JUEVES] [char](1) NULL,
	[VIERNES] [char](1) NULL,
	[SABADO] [char](1) NULL,
	[HORA_PREPARA] [char](5) NULL,
	[HORA_SALIDA] [char](5) NULL,
 CONSTRAINT [PK_FLETE] PRIMARY KEY NONCLUSTERED 
(
	[CODFLETE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pedidos_web]    Script Date: 28/2/2020 15:29:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pedidos_web](
	[id_pedido_web] [int] IDENTITY(8,1) NOT NULL,
	[fecha_carga] [date] NOT NULL,
	[fecha_pedido] [date] NOT NULL,
	[codCli] [int] NOT NULL,
	[id_estado] [int] NOT NULL,
	[observacion] [varchar](500) NULL,
 CONSTRAINT [PK_pedidos_web_id_pedido_web] PRIMARY KEY CLUSTERED 
(
	[id_pedido_web] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[e2_UsuariosWeb] ADD  CONSTRAINT [DF_UsuariosGlobal_FechaCreacion]  DEFAULT (getutcdate()) FOR [FechaCreacionUTC]
GO
ALTER TABLE [dbo].[pedidos_web] ADD  DEFAULT (NULL) FOR [observacion]
GO
ALTER TABLE [dbo].[e2_UsuariosWeb]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosGlobal_UsuariosGlobal] FOREIGN KEY([IdRol])
REFERENCES [dbo].[e2_RolesUsuarioWeb] ([IdRol])
GO
ALTER TABLE [dbo].[e2_UsuariosWeb] CHECK CONSTRAINT [FK_UsuariosGlobal_UsuariosGlobal]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'autorepuestosmylweb.det_pedidos_web' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'det_pedidos_web'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'autorepuestosmylweb.pedidos_web' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pedidos_web'
GO
