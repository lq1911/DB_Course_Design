using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DISHES",
                columns: table => new
                {
                    DISHID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DISHNAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    ISSOLDOUT = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, defaultValue: "IsSoldOut"),
                    DISHIMAGE = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DISHES", x => x.DISHID);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    USERID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USERNAME = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false),
                    PASSWORD = table.Column<string>(type: "NVARCHAR2(64)", maxLength: 64, nullable: false),
                    PHONENUMBER = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    GENDER = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: true),
                    FULLNAME = table.Column<string>(type: "NVARCHAR2(6)", maxLength: 6, nullable: true),
                    AVATAR = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                    BIRTHDAY = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    ACCOUNTCREATIONTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ISPROFILEPUBLIC = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ROLE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.USERID);
                });

            migrationBuilder.CreateTable(
                name: "ADMINISTRATORS",
                columns: table => new
                {
                    USERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ADMINREGISTRATIONTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    MANAGEDENTITIES = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    ISSUEHANDLINGSCORE = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 0.00m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMINISTRATORS", x => x.USERID);
                    table.ForeignKey(
                        name: "FK_ADMINISTRATORS_USER_USERID",
                        column: x => x.USERID,
                        principalTable: "USER",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COURIERS",
                columns: table => new
                {
                    USERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    COURIERREGISTRATIONTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    VEHICLETYPE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    REPUTATIONPOINTS = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 0),
                    TOTALDELIVERIES = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 0),
                    AVGDELIVERYTIME = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 0),
                    AVERAGERATING = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 0.00m),
                    MONTHLYSALARY = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 0),
                    ISONLINE = table.Column<bool>(type: "BOOLEAN", nullable: false, defaultValue: false),
                    COURIERLONGITUDE = table.Column<decimal>(type: "decimal(10,6)", nullable: true),
                    COURIERLATITUDE = table.Column<decimal>(type: "decimal(10,6)", nullable: true),
                    LASTONLINETIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COURIERS", x => x.USERID);
                    table.ForeignKey(
                        name: "FK_COURIERS_USER_USERID",
                        column: x => x.USERID,
                        principalTable: "USER",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMERS",
                columns: table => new
                {
                    USERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DEFAULTADDRESS = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    REPUTATIONPOINTS = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 0),
                    ISMEMBER = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, defaultValue: "NotMember")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERS", x => x.USERID);
                    table.ForeignKey(
                        name: "FK_CUSTOMERS_USER_USERID",
                        column: x => x.USERID,
                        principalTable: "USER",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SELLER",
                columns: table => new
                {
                    USERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SELLERREGISTRATIONTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    REPUTATIONPOINTS = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 0),
                    BANSTATUS = table.Column<int>(type: "NUMBER(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SELLER", x => x.USERID);
                    table.ForeignKey(
                        name: "FK_SELLER_USER_USERID",
                        column: x => x.USERID,
                        principalTable: "USER",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FAVORITESFOLDER",
                columns: table => new
                {
                    FOLDERID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FOLDERNAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CUSTOMERID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAVORITESFOLDER", x => x.FOLDERID);
                    table.ForeignKey(
                        name: "FK_FAVORITESFOLDER_CUSTOMERS_CUSTOMERID",
                        column: x => x.CUSTOMERID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SHOPPINGCART",
                columns: table => new
                {
                    CARTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    LASTUPDATEDTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TOTALPRICE = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0.00m),
                    CUSTOMERID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHOPPINGCART", x => x.CARTID);
                    table.ForeignKey(
                        name: "FK_SHOPPINGCART_CUSTOMERS_CUSTOMERID",
                        column: x => x.CUSTOMERID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "STORE",
                columns: table => new
                {
                    STOREID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    STORENAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    STOREADDRESS = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    OPENTIME = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: false),
                    CLOSETIME = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: false),
                    AVERAGERATING = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0.00m),
                    MONTHLYSALES = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STOREFEATURES = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    STORECREATIONTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    STORESTATE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    STORECATEGORY = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    STOREIMAGE = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    SELLERID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STORE", x => x.STOREID);
                    table.ForeignKey(
                        name: "FK_STORE_SELLER_SELLERID",
                        column: x => x.SELLERID,
                        principalTable: "SELLER",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SHOPPINGCARTITEM",
                columns: table => new
                {
                    ITEMID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    QUANTITY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TOTALPRICE = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DISHID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CARTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHOPPINGCARTITEM", x => x.ITEMID);
                    table.ForeignKey(
                        name: "FK_SHOPPINGCARTITEM_DISHES_DISHID",
                        column: x => x.DISHID,
                        principalTable: "DISHES",
                        principalColumn: "DISHID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SHOPPINGCARTITEM_SHOPPINGCART_CARTID",
                        column: x => x.CARTID,
                        principalTable: "SHOPPINGCART",
                        principalColumn: "CARTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COUPON_MANAGERS",
                columns: table => new
                {
                    COUPONMANAGERID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    MINIMUMSPEND = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DISCOUNTAMOUNT = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    VALIDFROM = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    VALIDTO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    STOREID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUPON_MANAGERS", x => x.COUPONMANAGERID);
                    table.ForeignKey(
                        name: "FK_COUPON_MANAGERS_STORE_STOREID",
                        column: x => x.STOREID,
                        principalTable: "STORE",
                        principalColumn: "STOREID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DELIVERY_TASKS",
                columns: table => new
                {
                    TASKID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ESTIMATEDARRIVALTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ESTIMATEDDELIVERYTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    COURIERLONGITUDE = table.Column<decimal>(type: "decimal(10,6)", nullable: true),
                    COURIERLATITUDE = table.Column<decimal>(type: "decimal(10,6)", nullable: true),
                    PUBLISHTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ACCEPTTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    STATUS = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, defaultValue: "Pending"),
                    COMPLETIONTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    DELIVERYFEE = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0.00m),
                    CUSTOMERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STOREID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    COURIERID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DELIVERY_TASKS", x => x.TASKID);
                    table.ForeignKey(
                        name: "FK_DELIVERY_TASKS_COURIERS_COURIERID",
                        column: x => x.COURIERID,
                        principalTable: "COURIERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DELIVERY_TASKS_CUSTOMERS_CUSTOMERID",
                        column: x => x.CUSTOMERID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DELIVERY_TASKS_STORE_STOREID",
                        column: x => x.STOREID,
                        principalTable: "STORE",
                        principalColumn: "STOREID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "FAVORITE_ITEMS",
                columns: table => new
                {
                    ITEMID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FAVORITEDAT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    FAVORITEREASON = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    STOREID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FOLDERID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAVORITE_ITEMS", x => x.ITEMID);
                    table.ForeignKey(
                        name: "FK_FAVORITE_ITEMS_FAVORITESFOLDER_FOLDERID",
                        column: x => x.FOLDERID,
                        principalTable: "FAVORITESFOLDER",
                        principalColumn: "FOLDERID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FAVORITE_ITEMS_STORE_STOREID",
                        column: x => x.STOREID,
                        principalTable: "STORE",
                        principalColumn: "STOREID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FOODORDER",
                columns: table => new
                {
                    ORDERID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ORDERTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    PAYMENTTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    REMARKS = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                    FOODORDERSTATE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CUSTOMERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CARTID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    STOREID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FOODORDER", x => x.ORDERID);
                    table.ForeignKey(
                        name: "FK_FOODORDER_CUSTOMERS_CUSTOMERID",
                        column: x => x.CUSTOMERID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FOODORDER_SHOPPINGCART_CARTID",
                        column: x => x.CARTID,
                        principalTable: "SHOPPINGCART",
                        principalColumn: "CARTID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_FOODORDER_STORE_STOREID",
                        column: x => x.STOREID,
                        principalTable: "STORE",
                        principalColumn: "STOREID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MENU",
                columns: table => new
                {
                    MENUID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    VERSION = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    ACTIVEPERIOD = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    STOREID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MENU", x => x.MENUID);
                    table.ForeignKey(
                        name: "FK_MENU_STORE_STOREID",
                        column: x => x.STOREID,
                        principalTable: "STORE",
                        principalColumn: "STOREID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STOREVIOLATIONPENALTIE",
                columns: table => new
                {
                    PENALTYID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PENALTYREASON = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    PENALTYTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    SELLERPENALTY = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    STOREPENALTY = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    STOREID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOREVIOLATIONPENALTIE", x => x.PENALTYID);
                    table.ForeignKey(
                        name: "FK_STOREVIOLATIONPENALTIE_STORE_STOREID",
                        column: x => x.STOREID,
                        principalTable: "STORE",
                        principalColumn: "STOREID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DELIVERY_COMPLAINTS",
                columns: table => new
                {
                    COMPLAINTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    COMPLAINTREASON = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    COMPLAINTTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    COURIERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CUSTOMERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DELIVERYTASKID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DELIVERY_COMPLAINTS", x => x.COMPLAINTID);
                    table.ForeignKey(
                        name: "FK_DELIVERY_COMPLAINTS_COURIERS_COURIERID",
                        column: x => x.COURIERID,
                        principalTable: "COURIERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DELIVERY_COMPLAINTS_CUSTOMERS_CUSTOMERID",
                        column: x => x.CUSTOMERID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DELIVERY_COMPLAINTS_DELIVERY_TASKS_DELIVERYTASKID",
                        column: x => x.DELIVERYTASKID,
                        principalTable: "DELIVERY_TASKS",
                        principalColumn: "TASKID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AFTER_SALE_APPLICATIONS",
                columns: table => new
                {
                    APPLICATIONID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    APPLICATIONTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ORDERID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AFTER_SALE_APPLICATIONS", x => x.APPLICATIONID);
                    table.ForeignKey(
                        name: "FK_AFTER_SALE_APPLICATIONS_FOODORDER_ORDERID",
                        column: x => x.ORDERID,
                        principalTable: "FOODORDER",
                        principalColumn: "ORDERID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COMMENTS",
                columns: table => new
                {
                    COMMENTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CONTENT = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    POSTEDAT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    LIKES = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 0),
                    REPLIES = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 0),
                    RATING = table.Column<int>(type: "NUMBER(10)", nullable: true, comment: "评分：1-5分"),
                    COMMENTIMAGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COMMENTTYPE = table.Column<int>(type: "NUMBER(10)", nullable: false, comment: "评论类型：1=店铺评论，2=订单评论，3=回复评论"),
                    STOREID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    FOODORDERID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    REPLYTOCOMMENTID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    COMMENTERID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMMENTS", x => x.COMMENTID);
                    table.ForeignKey(
                        name: "FK_COMMENTS_COMMENTS_REPLYTOCOMMENTID",
                        column: x => x.REPLYTOCOMMENTID,
                        principalTable: "COMMENTS",
                        principalColumn: "COMMENTID");
                    table.ForeignKey(
                        name: "FK_COMMENTS_CUSTOMERS_COMMENTERID",
                        column: x => x.COMMENTERID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_COMMENTS_FOODORDER_FOODORDERID",
                        column: x => x.FOODORDERID,
                        principalTable: "FOODORDER",
                        principalColumn: "ORDERID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COMMENTS_STORE_STOREID",
                        column: x => x.STOREID,
                        principalTable: "STORE",
                        principalColumn: "STOREID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "COUPONS",
                columns: table => new
                {
                    COUPONID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    COUPONSTATE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    COUPONMANAGERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CUSTOMERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ORDERID = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUPONS", x => x.COUPONID);
                    table.ForeignKey(
                        name: "FK_COUPONS_COUPON_MANAGERS_COUPONMANAGERID",
                        column: x => x.COUPONMANAGERID,
                        principalTable: "COUPON_MANAGERS",
                        principalColumn: "COUPONMANAGERID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COUPONS_CUSTOMERS_CUSTOMERID",
                        column: x => x.CUSTOMERID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_COUPONS_FOODORDER_ORDERID",
                        column: x => x.ORDERID,
                        principalTable: "FOODORDER",
                        principalColumn: "ORDERID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MENU_DISH",
                columns: table => new
                {
                    MENUID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DISHID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MENU_DISH", x => new { x.MENUID, x.DISHID });
                    table.ForeignKey(
                        name: "FK_MENU_DISH_DISHES_DISHID",
                        column: x => x.DISHID,
                        principalTable: "DISHES",
                        principalColumn: "DISHID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MENU_DISH_MENU_MENUID",
                        column: x => x.MENUID,
                        principalTable: "MENU",
                        principalColumn: "MENUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SUPERVISE_",
                columns: table => new
                {
                    ADMINID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PENALTYID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUPERVISE_", x => new { x.ADMINID, x.PENALTYID });
                    table.ForeignKey(
                        name: "FK_SUPERVISE__ADMINISTRATORS_ADMINID",
                        column: x => x.ADMINID,
                        principalTable: "ADMINISTRATORS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SUPERVISE__STOREVIOLATIONPENALTIE_PENALTYID",
                        column: x => x.PENALTYID,
                        principalTable: "STOREVIOLATIONPENALTIE",
                        principalColumn: "PENALTYID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EVALUATE_COMPLAINT",
                columns: table => new
                {
                    ADMINID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    COMPLAINTID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVALUATE_COMPLAINT", x => new { x.ADMINID, x.COMPLAINTID });
                    table.ForeignKey(
                        name: "FK_EVALUATE_COMPLAINT_ADMINISTRATORS_ADMINID",
                        column: x => x.ADMINID,
                        principalTable: "ADMINISTRATORS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVALUATE_COMPLAINT_DELIVERY_COMPLAINTS_COMPLAINTID",
                        column: x => x.COMPLAINTID,
                        principalTable: "DELIVERY_COMPLAINTS",
                        principalColumn: "COMPLAINTID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EVALUATE_AFTER_SALE",
                columns: table => new
                {
                    ADMINID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    APPLICATIONID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVALUATE_AFTER_SALE", x => new { x.ADMINID, x.APPLICATIONID });
                    table.ForeignKey(
                        name: "FK_EVALUATE_AFTER_SALE_ADMINISTRATORS_ADMINID",
                        column: x => x.ADMINID,
                        principalTable: "ADMINISTRATORS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVALUATE_AFTER_SALE_AFTER_SALE_APPLICATIONS_APPLICATIONID",
                        column: x => x.APPLICATIONID,
                        principalTable: "AFTER_SALE_APPLICATIONS",
                        principalColumn: "APPLICATIONID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REVIEW_COMMENT",
                columns: table => new
                {
                    ADMINID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    COMMENTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    REVIEWTIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REVIEW_COMMENT", x => new { x.ADMINID, x.COMMENTID });
                    table.ForeignKey(
                        name: "FK_REVIEW_COMMENT_ADMINISTRATORS_ADMINID",
                        column: x => x.ADMINID,
                        principalTable: "ADMINISTRATORS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_REVIEW_COMMENT_COMMENTS_COMMENTID",
                        column: x => x.COMMENTID,
                        principalTable: "COMMENTS",
                        principalColumn: "COMMENTID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AFTER_SALE_APPLICATIONS_ORDERID",
                table: "AFTER_SALE_APPLICATIONS",
                column: "ORDERID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTS_COMMENTERID",
                table: "COMMENTS",
                column: "COMMENTERID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTS_FOODORDERID",
                table: "COMMENTS",
                column: "FOODORDERID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTS_REPLYTOCOMMENTID",
                table: "COMMENTS",
                column: "REPLYTOCOMMENTID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTS_STOREID",
                table: "COMMENTS",
                column: "STOREID");

            migrationBuilder.CreateIndex(
                name: "IX_COUPONS_COUPONMANAGERID",
                table: "COUPONS",
                column: "COUPONMANAGERID");

            migrationBuilder.CreateIndex(
                name: "IX_COUPONS_CUSTOMERID",
                table: "COUPONS",
                column: "CUSTOMERID");

            migrationBuilder.CreateIndex(
                name: "IX_COUPONS_ORDERID",
                table: "COUPONS",
                column: "ORDERID");

            migrationBuilder.CreateIndex(
                name: "IX_COUPON_MANAGERS_STOREID",
                table: "COUPON_MANAGERS",
                column: "STOREID");

            migrationBuilder.CreateIndex(
                name: "IX_DELIVERY_COMPLAINTS_COURIERID",
                table: "DELIVERY_COMPLAINTS",
                column: "COURIERID");

            migrationBuilder.CreateIndex(
                name: "IX_DELIVERY_COMPLAINTS_CUSTOMERID",
                table: "DELIVERY_COMPLAINTS",
                column: "CUSTOMERID");

            migrationBuilder.CreateIndex(
                name: "IX_DELIVERY_COMPLAINTS_DELIVERYTASKID",
                table: "DELIVERY_COMPLAINTS",
                column: "DELIVERYTASKID");

            migrationBuilder.CreateIndex(
                name: "IX_DELIVERY_TASKS_COURIERID",
                table: "DELIVERY_TASKS",
                column: "COURIERID");

            migrationBuilder.CreateIndex(
                name: "IX_DELIVERY_TASKS_CUSTOMERID",
                table: "DELIVERY_TASKS",
                column: "CUSTOMERID");

            migrationBuilder.CreateIndex(
                name: "IX_DELIVERY_TASKS_STOREID",
                table: "DELIVERY_TASKS",
                column: "STOREID");

            migrationBuilder.CreateIndex(
                name: "IX_EVALUATE_AFTER_SALE_APPLICATIONID",
                table: "EVALUATE_AFTER_SALE",
                column: "APPLICATIONID");

            migrationBuilder.CreateIndex(
                name: "IX_EVALUATE_COMPLAINT_COMPLAINTID",
                table: "EVALUATE_COMPLAINT",
                column: "COMPLAINTID");

            migrationBuilder.CreateIndex(
                name: "IX_FAVORITESFOLDER_CUSTOMERID",
                table: "FAVORITESFOLDER",
                column: "CUSTOMERID");

            migrationBuilder.CreateIndex(
                name: "IX_FAVORITE_ITEMS_FOLDERID",
                table: "FAVORITE_ITEMS",
                column: "FOLDERID");

            migrationBuilder.CreateIndex(
                name: "IX_FAVORITE_ITEMS_STOREID",
                table: "FAVORITE_ITEMS",
                column: "STOREID");

            migrationBuilder.CreateIndex(
                name: "IX_FOODORDER_CARTID",
                table: "FOODORDER",
                column: "CARTID",
                unique: true,
                filter: "\"CARTID\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FOODORDER_CUSTOMERID",
                table: "FOODORDER",
                column: "CUSTOMERID");

            migrationBuilder.CreateIndex(
                name: "IX_FOODORDER_STOREID",
                table: "FOODORDER",
                column: "STOREID");

            migrationBuilder.CreateIndex(
                name: "IX_MENU_STOREID",
                table: "MENU",
                column: "STOREID");

            migrationBuilder.CreateIndex(
                name: "IX_MENU_DISH_DISHID",
                table: "MENU_DISH",
                column: "DISHID");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_COMMENT_COMMENTID",
                table: "REVIEW_COMMENT",
                column: "COMMENTID");

            migrationBuilder.CreateIndex(
                name: "IX_SHOPPINGCART_CUSTOMERID",
                table: "SHOPPINGCART",
                column: "CUSTOMERID");

            migrationBuilder.CreateIndex(
                name: "IX_SHOPPINGCARTITEM_CARTID",
                table: "SHOPPINGCARTITEM",
                column: "CARTID");

            migrationBuilder.CreateIndex(
                name: "IX_SHOPPINGCARTITEM_DISHID",
                table: "SHOPPINGCARTITEM",
                column: "DISHID");

            migrationBuilder.CreateIndex(
                name: "IX_STORE_SELLERID",
                table: "STORE",
                column: "SELLERID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_STOREVIOLATIONPENALTIE_STOREID",
                table: "STOREVIOLATIONPENALTIE",
                column: "STOREID");

            migrationBuilder.CreateIndex(
                name: "IX_SUPERVISE__PENALTYID",
                table: "SUPERVISE_",
                column: "PENALTYID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COUPONS");

            migrationBuilder.DropTable(
                name: "EVALUATE_AFTER_SALE");

            migrationBuilder.DropTable(
                name: "EVALUATE_COMPLAINT");

            migrationBuilder.DropTable(
                name: "FAVORITE_ITEMS");

            migrationBuilder.DropTable(
                name: "MENU_DISH");

            migrationBuilder.DropTable(
                name: "REVIEW_COMMENT");

            migrationBuilder.DropTable(
                name: "SHOPPINGCARTITEM");

            migrationBuilder.DropTable(
                name: "SUPERVISE_");

            migrationBuilder.DropTable(
                name: "COUPON_MANAGERS");

            migrationBuilder.DropTable(
                name: "AFTER_SALE_APPLICATIONS");

            migrationBuilder.DropTable(
                name: "DELIVERY_COMPLAINTS");

            migrationBuilder.DropTable(
                name: "FAVORITESFOLDER");

            migrationBuilder.DropTable(
                name: "MENU");

            migrationBuilder.DropTable(
                name: "COMMENTS");

            migrationBuilder.DropTable(
                name: "DISHES");

            migrationBuilder.DropTable(
                name: "ADMINISTRATORS");

            migrationBuilder.DropTable(
                name: "STOREVIOLATIONPENALTIE");

            migrationBuilder.DropTable(
                name: "DELIVERY_TASKS");

            migrationBuilder.DropTable(
                name: "FOODORDER");

            migrationBuilder.DropTable(
                name: "COURIERS");

            migrationBuilder.DropTable(
                name: "SHOPPINGCART");

            migrationBuilder.DropTable(
                name: "STORE");

            migrationBuilder.DropTable(
                name: "CUSTOMERS");

            migrationBuilder.DropTable(
                name: "SELLER");

            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
