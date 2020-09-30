<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Program.aspx.cs" Inherits="DisbursementDashboard.Program" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!doctype html>
<html lang="en">
    <head>
        <title>Disbursement Dashboard</title>
        <!-- Required meta tags -->
        <meta charset="utf-8">
        <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <!--     Fonts and icons     -->
        <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700,200" rel="stylesheet" />
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" />
        <!-- Material Kit CSS -->
        <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
        <link href="../assets/css/now-ui-dashboard.css" rel="stylesheet" />
        <link href="../assets/css/jquery-ui.min.css" rel="stylesheet">

        <style>
            body {
                font-size: 12px;
                background-image: linear-gradient(#b5dae1, #ffffff)
            }
            .chart_container {
                
            }
            .card_btn {
                background-color: inherit;
                width: 100%;
                height: 100%;
                color: white;
                position: absolute;
                bottom: auto;
            }
            .card {
                height: 140px;
                position: relative;
                background-color: #629EEA;
                color: #f8f9fa;
            }
            .card-body {
                position: relative;
                height:100%;
                width:100%;
            }
            .card-text {
                text-align: left;
                font-size: 15px;
            }
            .card-text-large {
                text-align: center;
                padding-top: 15px;
            }
            .card-btn-container {
                height: 25%;
                width:100%;
                position: absolute;
                bottom: 0;
                left: 0;
            }
            .card-btn-container a {
                text-align: center;
            }
            .card_green {
                background-color: #1a9124;
                color: #f8f9fa;
            }
            .card_yellow {
                background-color: #f5b905;
                color: #f8f9fa;
            }
            .card_red {
                background-color: #c23529;
                color: #f8f9fa;
            }

            .row-1 {
                min-height: 60px;
            }
            .row-2 {
                height: 140px;
            }
            .row-3 {
                height: 140px;
            }
            .row-4 {
                height: 140px;
            }
            section {
                display: flex;
                width: 50%;
                height: 140px;
                margin: auto;
            }
            p {
                
                text-align: center;
            }
            .header {
                font-weight: bold;
            }
            .header-row {
                text-align: right;
                margin-left: 0px;
            }
            .header-column-container {
                
            }
            .header-column {
                text-align: center;
                font-size: 1.3em;
                vertical-align: bottom;
                white-space: normal;
            }
            .column-sizing {
                width: 250px !important;
                padding: 5px;
                display: inline-block;
            }
            .inner {
                white-space: nowrap;
                width: 100%;
                overflow-x: scroll;
                overflow-y: hidden;
            }
            .container {
                white-space: nowrap;
                width: 100%;
                overflow-x: scroll;
                overflow-y: hidden;
            }

            /*accordion styling*/

        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="wrapper" style="overflow-x: hidden; padding-left:5px; padding-right:5px; width:100%;">
                <%--<div class="sidebar" data-color="orange">
                    <!--
                    Tip 1: You can change the color of the sidebar using: data-color="blue | green | orange | red | yellow"
                    -->
                    <div class="logo">
                        Disbursements
                    </div>

                    <div class="sidebar-wrapper" id="sidebar-wrapper">
                        <ul class="nav">        
                            <li class="active ">
                                <a href="../dashboard.html">
                                    <i class="now-ui-icons design_app"></i>
                                    <p>Dashboard</p>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>--%>
<!--12 COLUMN LAYOUT-->

                <!--HEADERS-->
                
                    <div class="inner">
                        <div class="header-column-container">
                            <div class="column-sizing header header-column">
                            
                            </div>
                            <div class="column-sizing header header-column">
                                Disbursements Processed (Last 7 Days)
                            </div>
                            <div class="column-sizing header header-column">
                                Disbursements Processed (Last 30 Days)
                            </div>
                            <div class="column-sizing header header-column">
                                Disbursements Paid (Last 7 Days)
                            </div>
                            <div class="column-sizing header header-column">
                                Disbursements Paid (Last 30 Days)
                            </div>
                            <div class="column-sizing header header-column">
                                New Disbursement Request Created Today
                            </div>
                            <div class="column-sizing header header-column">
                                Disbursement Request Processed Today
                            </div>
                            <div class="column-sizing header header-column">
                                New Disbursement Notices Created Today
                            </div>
                            <div class="column-sizing header header-column">
                                Disbursement Notices Processed Today
                            </div>
                            <div class="column-sizing header header-column">
                                Oldest Unprocessed Disbursement Request
                            </div>
                            <div class="column-sizing header header-column">
                                Oldest Unprocessed Disbursement Notice
                            </div>
                            <div class="column-sizing header header-column">
                                Accounts / Vins Currently Blocked
                            </div>
                            <div class="column-sizing header header-column">
                                Disbursements on Hold
                            </div>
                            <div class="column-sizing header header-column">
                                Rejected Disbursements
                            </div>
                            <div class="column-sizing header header-column">
                                Total Accounts On Hold
                            </div>
                        </div>
                        <div id="accordion-buttons">
                            <div class="column-sizing">
                                <div class="row-2 header header-row" id="row-2-header">
                                    <section>
                                        <a data-toggle="collapse" href="#collapseRows" class="row-2 header header-row" style="padding-top: 50%;">
                                            Overall DealShield
                                        </a>
                                    </section>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card card_green" id="card1-1">
                                    <div class="card-body">
                                        <p class="card-text">Notice: <% =noticesProcessed7DaysTotal %></p>
                                        <p class="card-text">Request: <% =requestsProcessed7DaysTotal %></p>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card card_green" id="card1-2">
                                    <div class="card-body">
                                        <p class="card-text">Notice: <% =noticesProcessed30DaysTotal %></p>
                                        <p class="card-text">Request: <% =requestsProcessed30DaysTotal %></p>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card card_green" id="card1-13">
                                    <div class="card-body">
                                        <h1 class="card-text-large">$ </h1>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card card_green" id="card1-14">
                                    <div class="card-body">
                                        <h1 class="card-text-large">$ </h1>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card " id="card1-3">
                                    <div class="card-body">
                                        <h1 class="card-text-large"><% =requestsCreatedTodayTotal %></h1>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card " id="card1-4">
                                    <div class="card-body">
                                        <h1 class="card-text-large"><% =requestsProcessedTodayTotal %></h1>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card " id="card1-5">
                                    <div class="card-body">
                                        <h1 class="card-text-large"><% =noticesCreatedTodayTotal %></h1>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card " id="card1-6">
                                    <div class="card-body">
                                        <h1 class="card-text-large"><% =noticesProcessedTodayTotal %></h1>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card " id="card1-7">
                                    <div class="card-body">
                                        <p class="card-text">Unworked: <% =requestOldestUnworkedTotal %></p>
                                        <p class="card-text">Modified: <% =requestOldestModifiedTotal %></p>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card " id="card1-8">
                                    <div class="card-body">
                                        <p class="card-text">Unworked: <% =noticeOldestUnworkedTotal %></p>
                                        <p class="card-text">Modified: <% =noticeOldestModifiedTotal %></p>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card " id="card1-9">
                                    <div class="card-body">
                                        <p class="card-text">Account: 10</p>
                                        <p class="card-text">VIN: 5</p>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card " id="card1-10">
                                    <div class="card-body">
                                        <p class="card-text">Request: <% =requestsOnHoldTotal %></p>
                                        <p class="card-text">Notice: <% =noticesOnHoldTotal %></p>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card " id="card1-11">
                                    <div class="card-body">
                                        <p class="card-text">Request: <% =disbursementsRejectedRequest %></p>
                                        <p class="card-text">Notice: <% =disbursementsRejectedNotice %></p>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column-sizing">
                                <div class="card " id="card1-12">
                                    <div class="card-body">
                                        <h1 class="card-text-large">11</h1>
                                        <div class="card-btn-container">
                                            <a href="#" class="card_btn">More Details --></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <div id="collapseRows" class="collapse" data-parent="#accordion-buttons">    
                                <div style="height: 170px;">
                                    <div class="column-sizing">
                                        <div class=" row-3 header header-row" id="row-3-header">
                                            <section>
                                                <p>
                                                    Manheim Auction
                                                </p>
                                            </section>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card card_green" id="card2-1">
                                            <div class="card-body">
                                                <p class="card-text">Notice: <% =noticesProcessed7DaysG2G %></p>
                                                <p class="card-text">Request: <% =requestsProcessed7DaysG2G %></p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card card_green" id="card2-2">
                                            <div class="card-body">
                                                <p class="card-text">Notice: <% =noticesProcessed30DaysG2G %></p>
                                                <p class="card-text">Request: <% =requestsProcessed30DaysG2G %></p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card card_green" id="card2-13">
                                            <div class="card-body">
                                                <h1 class="card-text-large">$ </h1>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card card_green" id="card2-14">
                                            <div class="card-body">
                                                <h1 class="card-text-large">$ </h1>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card2-3">
                                            <div class="card-body">
                                                <h1 class="card-text-large"><% =requestsCreatedTodayG2G %></h1>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card2-4">
                                            <div class="card-body">
                                                <h1 class="card-text-large"><% =requestsProcessedTodayG2G %></h1>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card2-5">
                                            <div class="card-body">
                                                <h1 class="card-text-large"><% =noticesCreatedTodayG2G %></h1>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card2-6">
                                            <div class="card-body">
                                                <h1 class="card-text-large"><% =noticesProcessedTodayG2G %></h1>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card2-7">
                                            <div class="card-body">
                                                <p class="card-text">Unworked: <% =requestOldestUnworkedG2G %></p>
                                                <p class="card-text">Modified: <% =requestOldestModifiedG2G %></p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card2-8">
                                            <div class="card-body">
                                                <p class="card-text">Unworked: <% =noticeOldestUnworkedG2G %></p>
                                                <p class="card-text">Modified: <% =noticeOldestModifiedG2G %></p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card2-9">
                                            <div class="card-body">
                                                <p class="card-text">Account: 8</p>
                                                <p class="card-text">VIN: 4</p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card2-10">
                                            <div class="card-body">
                                                <p class="card-text">Request: <% =requestsOnHoldG2G %></p>
                                                <p class="card-text">Notice: <% =noticesOnHoldG2G %></p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card2-11">
                                            <div class="card-body">
                                                <p class="card-text">Request: <% =disbursementsRejectedRequestG2G %></p>
                                                <p class="card-text">Notice: <% =disbursementsRejectedNoticeG2G %></p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                        
                                <div class="">
                                    <div class="column-sizing">
                                        <div class=" row-4 header header-row" id="row-4-header">
                                            <section>
                                                <p>
                                                    External Auction
                                                </p>
                                            </section>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card card_green" id="card3-1">
                                            <div class="card-body">
                                                <p class="card-text">Notice: <% =noticesProcessed7DaysEx %></p>
                                                <p class="card-text">Request: <% =requestsProcessed7DaysEx %></p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card card_green" id="card3-2">
                                            <div class="card-body">
                                                <p class="card-text">Notice: <% =noticesProcessed30DaysEx %></p>
                                                <p class="card-text">Request: <% =requestsProcessed30DaysEx %></p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card card_green" id="card3-13">
                                            <div class="card-body">
                                                <h1 class="card-text-large">$ </h1>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card card_green" id="card3-14">
                                            <div class="card-body">
                                                <h1 class="card-text-large">$ </h1>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card3-3">
                                            <div class="card-body">
                                                <h1 class="card-text-large"><% =requestsCreatedTodayEx %></h1>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card3-4">
                                            <div class="card-body">
                                                <h1 class="card-text-large"><% =requestsProcessedTodayEx %></h1>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card3-5">
                                            <div class="card-body">
                                                <h1 class="card-text-large"><% =noticesCreatedTodayEx %></h1>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card3-6">
                                            <div class="card-body">
                                                <h1 class="card-text-large"><% =noticesProcessedTodayEx %></h1>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card3-7">
                                            <div class="card-body">
                                                <p class="card-text">Unworked: <% =requestOldestUnworkedEx %></p>
                                                <p class="card-text">Modified: <% =requestOldestModifiedEx %></p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card3-8">
                                            <div class="card-body">
                                                <p class="card-text">Unworked: <% =noticeOldestUnworkedEx %></p>
                                                <p class="card-text">Modified: <% =noticeOldestModifiedEx %></p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card3-9">
                                            <div class="card-body">
                                                <p class="card-text">Account: 2</p>
                                                <p class="card-text">VIN: 1</p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card3-10">
                                            <div class="card-body">
                                                <p class="card-text">Request: <% =requestsOnHoldEx %></p>
                                                <p class="card-text">Notice: <% =noticesOnHoldEx %></p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-sizing">
                                        <div class="card " id="card3-11">
                                            <div class="card-body">
                                                <p class="card-text">Request: <% =disbursementsRejectedRequestEx %></p>
                                                <p class="card-text">Notice: <% =disbursementsRejectedNoticeEx %></p>
                                                <div class="card-btn-container">
                                                    <a href="#" class="card_btn">More Details --></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </div>

                <%--<div class="col-sm-1">
                    <div class="card " id="card1-1">
                        <div class="card-body">
                            <p class="card-text">Count: 50</p>
                            <p class="card-text">$ 600,000 </p>
                            <p class="card-text">ADP: 5.0</p>
                            <div class="card-btn-container">
                                <a href="#" class="card_btn">More Details --></a>
                            </div>
                        </div>
                    </div>
                </div>--%>

                <hr />
                
                <%--<div class="row row-5 header header-column">
                    <div class="col-sm-4">
                        Daily
                    </div>
                    <div class="col-sm-4">
                        Weekly
                    </div>
                    <div class="col-sm-4">
                        Monthly
                    </div>
                </div>--%>
                <div id="accordion-graphs">
                <div class="row row-6">
                    <div class="col-sm-1">
                        <div class="row row-6 header header-row" id="row-6-header">
                            <section>
                                <a data-toggle="collapse" href="#collapseGraphs" class="row row-6 header header-row" style="padding-top: 50%;">
                                    Disbursement Charts
                                </a>
                            </section>
                        </div>
                    </div>

                    <div class="col-sm-11">
                        <div class="row">
                            <div class="col-sm-4 header header-column">
                                <div>
                                    <canvas id="newDisbursementsByDay" height="250"></canvas>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div>
                                    <canvas id="newDisbursementsByWeek" height="250"></canvas>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div>
                                    <canvas id="newDisbursementsByMonth" height="250"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="collapseGraphs" data-parent="#accordion-graphs">
                    <div class="row row-7">
                        <div class="col-sm-1">
                            <div class="row row-7" id="row-7-header">

                            </div>
                        </div>

                        <div class="col-sm-11">
                            <div class="row">
                                <div class="col-sm-4">
                                    <div>
                                        <canvas id="newDisbursementsSplitByDay" height="250"></canvas>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div>
                                        <canvas id="newDisbursementsSplitByWeek" height="250"></canvas>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div>
                                        <canvas id="newDisbursementsSplitByMonth" height="250"></canvas>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </form>
    </body>
    <!--   Core JS Files   -->
    <script src="../assets/js/core/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/js/core/popper.min.js" type="text/javascript"></script>
    <script src="../assets/js/core/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/js/jquery-ui.min.js"></script>
    <script src="../assets/js/plugins/perfect-scrollbar.jquery.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.bundle.js" type="text/javascript"></script>

    <!-- Control Center for Now Ui Dashboard: parallax effects, scripts for the example pages etc -->
    <script src="../assets/js/now-ui-dashboard.js" type="text/javascript"></script>
</html>

<script>
    window.onload = function () {
        //$("#accordion").accordion({
        //    collapsible: true,
        //    header: "span",
        //    active: false
        //});
        //$("#accordion").removeClass("ui-accordion ui-widget ui-helper-reset");
        //$("#accordion-span").removeClass("ui-accordion-header ui-corner-top ui-state-default ui-accordion-icons ui-accordion-header-active ui-state-active");
        //$("#accordion-toggle-content'")

        $('#collapseGraphs').addClass("collapse");

        //apply color to buttons that change colors
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();
        var dateString = mm + '/' + dd + '/' + yyyy;
        today = new Date(dateString);

        applyButtonColor("card1-7", getDifferenceInDays(today, new Date("<% =requestOldestUnworkedTotal%>")), 5, 9999, 4, 4, 1, 3);
        applyButtonColor("card2-7", getDifferenceInDays(today, new Date("<% =requestOldestUnworkedG2G%>")), 5, 9999, 4, 4, 1, 3);
        applyButtonColor("card3-7", getDifferenceInDays(today, new Date("<% =requestOldestUnworkedEx%>")), 5, 9999, 4, 4, 1, 3);
        applyButtonColor("card1-8", getDifferenceInDays(today, new Date("<% =noticeOldestUnworkedTotal%>")), 5, 9999, 4, 4, 1, 3);
        applyButtonColor("card2-8", getDifferenceInDays(today, new Date("<% =noticeOldestUnworkedG2G%>")), 5, 9999, 4, 4, 1, 3);
        applyButtonColor("card3-8", getDifferenceInDays(today, new Date("<% =noticeOldestUnworkedEx%>")), 5, 9999, 4, 4, 1, 3);

        function isValidDate(d) {
            return d instanceof Date && !isNaN(d);
        }

        function getDifferenceInDays(date1, date2) {
            if (isValidDate(date1) && isValidDate(date2)) {
                var Difference_In_Time = date2.getTime() - date1.getTime();
                // To calculate the no. of days between two dates 
                var Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);
                Difference_In_Days = Math.abs(Difference_In_Days);
                return Math.round(Difference_In_Days);
            }
            else {
                return 0;
            }
            
        }

        //change button color
        function applyButtonColor(cardId, buttonValue, redMin, redMax, yellowMin, yellowMax, greenMin, greenMax) {
            //card_red card_green card_yellow
            console.log(cardId);
            console.log(buttonValue);
            var $dashboardButton = $('#' + cardId);
            if (buttonValue >= redMin && buttonValue <= redMax) {
                //button is red
                $dashboardButton.addClass("card_red");
            }
            else if (buttonValue >= yellowMin && buttonValue <= yellowMax) {
                //button is yellow
                $dashboardButton.addClass("card_yellow");
            }
            else if (buttonValue >= greenMin && buttonValue <= greenMax) {
                //button is green
                $dashboardButton.addClass("card_green");
            }
        }
    }

    chartColor = "#FFFFFF";

    // General configuration for the charts with Line gradientStroke
    newDisbursementsByDayConfig = {
        maintainAspectRatio: false,
        legend: {
            display: false
        },
        title: {
            display: true,
            text: 'Incoming Disbursements by Day',
            fontSize: 14
        },
        tooltips: {
            bodySpacing: 4,
            mode: "nearest",
            intersect: 0,
            position: "nearest",
            xPadding: 10,
            yPadding: 10,
            caretPadding: 10
        },
        responsive: 1,
        scales: {
            yAxes: [{
                display: true,
                ticks: {},
                gridLines: {
                    zeroLineColor: "",
                    drawTicks: true,
                    display: true,
                    drawBorder: true
                }
            }],
            xAxes: [{
                display: true,
                ticks: {},
                gridLines: {
                    zeroLineColor: "transparent",
                    drawTicks: false,
                    display: false,
                    drawBorder: false
                }
            }]
        },
        layout: {
            padding: { left: 0, right: 0, top: 15, bottom: 15 }
        }
    };

    //creating chart 1
    ctx = document.getElementById('newDisbursementsByDay').getContext("2d");

    //get key/value pairing of date/#new Disbursements
    var dailyDates = <%= dailyDatesJS %>;
    var dailyNewDisb = <%= dailyNewDisbursementsJS %>;
    var dailyNewDisbG2G = <%= dailyNewDisbursementsG2GJS %>;
    var dailyNewDisbEx = <%= dailyNewDisbursementsExJS %>;

    gradientStroke = ctx.createLinearGradient(500, 0, 100, 0);
    gradientStroke.addColorStop(0, '#80b6f4');
    gradientStroke.addColorStop(1, chartColor);

    gradientFill = ctx.createLinearGradient(0, 170, 0, 50);
    gradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
    gradientFill.addColorStop(1, "rgba(249, 99, 59, 0.40)");

    myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: dailyDates,
            datasets: [{
                label: "Disbursements",
                borderColor: "#46209D",
                pointBorderColor: "#FFF",
                pointBackgroundColor: "#46209D",
                pointBorderWidth: 2,
                pointHoverRadius: 4,
                pointHoverBorderWidth: 1,
                pointRadius: 4,
                fill: true,
                backgroundColor: gradientFill,
                borderWidth: 2,
                data: dailyNewDisb
            }]
        },
        options: newDisbursementsByDayConfig
    });

    //creating chart 2
    // General configuration for the charts with Line gradientStroke
    newDisbursementsByWeekConfig = {
        maintainAspectRatio: false,
        legend: {
            display: false
        },
        title: {
            display: true,
            text: 'Incoming Disbursements by Week',
            fontSize: 14
        },
        tooltips: {
            bodySpacing: 4,
            mode: "nearest",
            intersect: 0,
            position: "nearest",
            xPadding: 10,
            yPadding: 10,
            caretPadding: 10
        },
        responsive: 1,
        scales: {
            yAxes: [{
                display: true,
                gridLines: 0,
                ticks: {},
                gridLines: {
                    zeroLineColor: "",
                    drawTicks: true,
                    display: true,
                    drawBorder: true
                }
            }],
            xAxes: [{
                display: true,
                gridLines: 0,
                ticks: {},
                gridLines: {
                    zeroLineColor: "transparent",
                    drawTicks: false,
                    display: false,
                    drawBorder: false
                }
            }]
        },
        layout: {
            padding: { left: 0, right: 0, top: 15, bottom: 15 }
        }
    };
    ctx2 = document.getElementById('newDisbursementsByWeek').getContext("2d");

    gradientStroke = ctx2.createLinearGradient(500, 0, 100, 0);
    gradientStroke.addColorStop(0, '#80b6f4');
    gradientStroke.addColorStop(1, chartColor);

    gradientFill = ctx2.createLinearGradient(0, 170, 0, 50);
    gradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
    gradientFill.addColorStop(1, "rgba(249, 99, 59, 0.40)");

    var weeklyDates = <%= weeklyDatesJS %>;
    var weeklyNewDisb = <%= weeklyNewDisbursementsJS %>;
    var weeklyNewDisbG2G = <%= weeklyNewDisbursementsG2GJS %>;
    var weeklyNewDisbEx = <%= weeklyNewDisbursementsExJS %>;


    myChart = new Chart(ctx2, {
        type: 'line',
        data: {
            labels: weeklyDates,
            datasets: [{
                label: "Disbursements",
                borderColor: "#46209D",
                pointBorderColor: "#FFF",
                pointBackgroundColor: "#46209D",
                pointBorderWidth: 2,
                pointHoverRadius: 4,
                pointHoverBorderWidth: 1,
                pointRadius: 4,
                fill: true,
                backgroundColor: gradientFill,
                borderWidth: 2,
                data: weeklyNewDisb
            }]
        },
        options: newDisbursementsByWeekConfig
    });

    //creating chart 3
    // General configuration for the charts with Line gradientStroke
    newDisbursementsByMonthConfig = {
        maintainAspectRatio: false,
        legend: {
            display: false
        },
        title: {
            display: true,
            text: 'Incoming Disbursements by Month',
            fontSize: 14
        },
        tooltips: {
            bodySpacing: 4,
            mode: "nearest",
            intersect: 0,
            position: "nearest",
            xPadding: 10,
            yPadding: 10,
            caretPadding: 10
        },
        responsive: 1,
        scales: {
            yAxes: [{
                display: true,
                gridLines: 0,
                ticks: {},
                gridLines: {
                    zeroLineColor: "",
                    drawTicks: true,
                    display: true,
                    drawBorder: true
                }
            }],
            xAxes: [{
                display: true,
                gridLines: 0,
                ticks: {},
                gridLines: {
                    zeroLineColor: "transparent",
                    drawTicks: false,
                    display: false,
                    drawBorder: false
                }
            }]
        },
        layout: {
            padding: { left: 0, right: 0, top: 15, bottom: 15 }
        }
    };
    ctx3 = document.getElementById('newDisbursementsByMonth').getContext("2d");

    gradientStroke = ctx3.createLinearGradient(500, 0, 100, 0);
    gradientStroke.addColorStop(0, '#80b6f4');
    gradientStroke.addColorStop(1, chartColor);

    gradientFill = ctx3.createLinearGradient(0, 170, 0, 50);
    gradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
    gradientFill.addColorStop(1, "rgba(249, 99, 59, 0.40)");

    var monthlyDates = <%= monthlyDatesJS %>;
    var monthlyNewDisb = <%= monthlyNewDisbursementsJS %>;
    var monthlyNewDisbG2G = <%= monthlyNewDisbursementsG2GJS %>;
    var monthlyNewDisbEx = <%= monthlyNewDisbursementsExJS %>;

    myChart = new Chart(ctx3, {
        type: 'line',
        data: {
            labels: monthlyDates,
            datasets: [{
                label: "Disbursements",
                borderColor: "#46209D",
                pointBorderColor: "#FFF",
                pointBackgroundColor: "#46209D",
                pointBorderWidth: 2,
                pointHoverRadius: 4,
                pointHoverBorderWidth: 1,
                pointRadius: 4,
                fill: true,
                backgroundColor: gradientFill,
                borderWidth: 2,
                data: monthlyNewDisb
            }]
        },
        options: newDisbursementsByMonthConfig
    });

    //creating chart 4
    // General configuration for the charts with Line gradientStroke
    newDisbursementsSplitByDayConfig = {
        maintainAspectRatio: false,
        legend: {
            display: false
        },
        title: {
            display: true,
            text: 'Disbursement by Type',
            fontSize: 14
        },
        tooltips: {
            bodySpacing: 4,
            mode: "nearest",
            intersect: 0,
            position: "nearest",
            xPadding: 10,
            yPadding: 10,
            caretPadding: 10
        },
        responsive: 1,
        scales: {
            yAxes: [{
                display: true,
                gridLines: 0,
                ticks: {},
                gridLines: {
                    zeroLineColor: "",
                    drawTicks: true,
                    display: true,
                    drawBorder: true
                }
            }],
            xAxes: [{
                display: true,
                gridLines: 0,
                ticks: {},
                gridLines: {
                    zeroLineColor: "transparent",
                    drawTicks: false,
                    display: false,
                    drawBorder: false
                }
            }]
        },
        layout: {
            padding: { left: 0, right: 0, top: 15, bottom: 15 }
        }
    };
    ctx4 = document.getElementById('newDisbursementsSplitByDay').getContext("2d");

    gradientStroke = ctx4.createLinearGradient(500, 0, 100, 0);
    gradientStroke.addColorStop(0, '#80b6f4');
    gradientStroke.addColorStop(1, chartColor);

    gradientFill = ctx4.createLinearGradient(0, 170, 0, 50);
    gradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
    gradientFill.addColorStop(1, "rgba(249, 99, 59, 0.40)");

    myChart = new Chart(ctx4, {
        type: 'line',
        data: {
            labels: dailyDates,
            datasets: [{
                label: "Manheim",
                borderColor: "#f96332",
                pointBorderColor: "#FFF",
                pointBackgroundColor: "#f96332",
                pointBorderWidth: 2,
                pointHoverRadius: 4,
                pointHoverBorderWidth: 1,
                pointRadius: 4,
                fill: true,
                backgroundColor: gradientFill,
                borderWidth: 2,
                data: dailyNewDisbG2G
            },
            {
                label: "External",
                borderColor: "#3676e3",
                pointBorderColor: "#FFF",
                pointBackgroundColor: "#3676e3",
                pointBorderWidth: 2,
                pointHoverRadius: 4,
                pointHoverBorderWidth: 1,
                pointRadius: 4,
                fill: true,
                backgroundColor: gradientFill,
                borderWidth: 2,
                data: dailyNewDisbEx
            }]
        },
        options: newDisbursementsSplitByDayConfig
    });

    //creating chart 5
    // General configuration for the charts with Line gradientStroke
    newDisbursementsSplitByWeekConfig = {
        maintainAspectRatio: false,
        legend: {
            display: false
        },
        title: {
            display: true,
            text: 'Disbursement by Type',
            fontSize: 14
        },
        tooltips: {
            bodySpacing: 4,
            mode: "nearest",
            intersect: 0,
            position: "nearest",
            xPadding: 10,
            yPadding: 10,
            caretPadding: 10
        },
        responsive: 1,
        scales: {
            yAxes: [{
                display: true,
                gridLines: 0,
                ticks: {},
                gridLines: {
                    zeroLineColor: "",
                    drawTicks: true,
                    display: true,
                    drawBorder: true
                }
            }],
            xAxes: [{
                display: true,
                gridLines: 0,
                ticks: {},
                gridLines: {
                    zeroLineColor: "transparent",
                    drawTicks: false,
                    display: false,
                    drawBorder: false
                }
            }]
        },
        layout: {
            padding: { left: 0, right: 0, top: 15, bottom: 15 }
        }
    };
    ctx5 = document.getElementById('newDisbursementsSplitByWeek').getContext("2d");

    gradientStroke = ctx5.createLinearGradient(500, 0, 100, 0);
    gradientStroke.addColorStop(0, '#80b6f4');
    gradientStroke.addColorStop(1, chartColor);

    gradientFill = ctx5.createLinearGradient(0, 170, 0, 50);
    gradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
    gradientFill.addColorStop(1, "rgba(249, 99, 59, 0.40)");

    myChart = new Chart(ctx5, {
        type: 'line',
        data: {
            labels: weeklyDates,
            datasets: [{
                label: "Manheim",
                borderColor: "#f96332",
                pointBorderColor: "#FFF",
                pointBackgroundColor: "#f96332",
                pointBorderWidth: 2,
                pointHoverRadius: 4,
                pointHoverBorderWidth: 1,
                pointRadius: 4,
                fill: true,
                backgroundColor: gradientFill,
                borderWidth: 2,
                data: weeklyNewDisbG2G
            },
            {
                label: "External",
                borderColor: "#3676e3",
                pointBorderColor: "#FFF",
                pointBackgroundColor: "#3676e3",
                pointBorderWidth: 2,
                pointHoverRadius: 4,
                pointHoverBorderWidth: 1,
                pointRadius: 4,
                fill: true,
                backgroundColor: gradientFill,
                borderWidth: 2,
                data: weeklyNewDisbEx
            }]
        },
        options: newDisbursementsSplitByWeekConfig
    });

    //creating chart 6
    // General configuration for the charts with Line gradientStroke
    newDisbursementsSplitByMonthConfig = {
        maintainAspectRatio: false,
        legend: {
            display: false
        },
        title: {
            display: true,
            text: 'Disbursement by Type',
            fontSize: 14
        },
        tooltips: {
            bodySpacing: 4,
            mode: "nearest",
            intersect: 0,
            position: "nearest",
            xPadding: 10,
            yPadding: 10,
            caretPadding: 10
        },
        responsive: 1,
        scales: {
            yAxes: [{
                display: true,
                gridLines: 0,
                ticks: {},
                gridLines: {
                    zeroLineColor: "",
                    drawTicks: true,
                    display: true,
                    drawBorder: true
                }
            }],
            xAxes: [{
                display: true,
                gridLines: 0,
                ticks: {},
                gridLines: {
                    zeroLineColor: "transparent",
                    drawTicks: false,
                    display: false,
                    drawBorder: false
                }
            }]
        },
        layout: {
            padding: { left: 0, right: 0, top: 15, bottom: 15 }
        }
    };
    ctx6 = document.getElementById('newDisbursementsSplitByMonth').getContext("2d");

    gradientStroke = ctx6.createLinearGradient(500, 0, 100, 0);
    gradientStroke.addColorStop(0, '#80b6f4');
    gradientStroke.addColorStop(1, chartColor);

    gradientFill = ctx6.createLinearGradient(0, 170, 0, 50);
    gradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
    gradientFill.addColorStop(1, "rgba(249, 99, 59, 0.40)");

    myChart = new Chart(ctx6, {
        type: 'line',
        data: {
            labels: monthlyDates,
            datasets: [{
                label: "Maheim",
                borderColor: "#f96332",
                pointBorderColor: "#FFF",
                pointBackgroundColor: "#f96332",
                pointBorderWidth: 2,
                pointHoverRadius: 4,
                pointHoverBorderWidth: 1,
                pointRadius: 4,
                fill: true,
                backgroundColor: gradientFill,
                borderWidth: 2,
                data: monthlyNewDisbG2G
            },
            {
                label: "External",
                borderColor: "#3676e3",
                pointBorderColor: "#FFF",
                pointBackgroundColor: "#3676e3",
                pointBorderWidth: 2,
                pointHoverRadius: 4,
                pointHoverBorderWidth: 1,
                pointRadius: 4,
                fill: true,
                backgroundColor: gradientFill,
                borderWidth: 2,
                data: monthlyNewDisbEx
            }]
        },
        options: newDisbursementsSplitByMonthConfig
    });

    $(document).ready(resizeRowHeaders);

    $(window).resize(resizeRowHeaders);

    function resizeRowHeaders() {
        $("#row-1-header").height($(".header-column").height());
        $("#row-2-header").height($(".row-2").height());
        $("#row-3-header").height($(".row-3").height());
        $("#row-4-header").height($(".row-4").height());
        $("#row-5-header").height($(".row-5").height());
        $("#row-6-header").height($(".row-6").height());
        $("#row-7-header").height($(".row-7").height());
    }
</script>