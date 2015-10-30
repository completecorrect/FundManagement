#
#User Story
# As a Fund Manager, I want to be able to add stocks to my Fund so that I can manage and report on my Fund.
#
#Acceptance Criteria
# 1) I can add an 'Equity' or 'Bond' stock to my Fund via a Panel at the top of my screen.
#
# 2) When adding an 'Equity' or 'Bond' stock to my Fund, I must specify the price I bought the stock at 
# and the quantity of the stock that I bought, which are both mandatory when adding a stock to my fund
#
# 3) I can see all stocks added to my Fund in a data grid in a Panel in the middle of my screen, showing the following stock level information:
#•	Stock Type e.g. 'Equity' or 'Bond'
#•	Stock Name - dynamically generated from Stock Type and the number of occurrences of that Stock Type in the Fund e.g. 'Equity1', 'Equity2', 'Equity3', 'Bond1'
#•	Price
#•	Quantity
#•	Market Value - calculated from Price * Quantity
#•	Transaction Cost - calculated from Market Value * 0.5% for 'Equity' Stocks, Market Value * 2% for 'Bond' Stocks
#•	Stock Weight (calculated as a Market Value percentage of the Total Market Value of the Fund)
# 4) In my grid, Stock Name should be highlighted Red for any Stocks whose Market Value is < 0 or Transaction Cost > Tolerance where Tolerance = 100,000 when Stock Type is 'Bond' or Tolerance = 200,000 when Stock Type is 'Equity'
#
# 5) On the right hand side of my screen I can see a Panel with the following summary level Fund information:
#•	Equity - Total Number, Total Stock Weight and Total Market Value of Equities in the Fund
#•	Bond - Total Number, Total Stock Weight and Total Market Value of Bonds in the Fund
#•	All - Total Number, Total Stock Weight and Total Market Value of the Fund
#
# Please publish your final solution to a Github account
# 

Feature: FundManagement
	In order to manage and report on my Fund
	As a Fund Manager
	I want to be able to add stocks to my Fund

#@mytag
#Scenario: Add two numbers
#	Given I have entered 50 into the calculator
#	And I have entered 70 into the calculator
#	When I press add
#	Then the result should be 120 on the screen
