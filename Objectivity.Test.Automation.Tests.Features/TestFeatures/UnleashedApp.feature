
#The MIT License (MIT)

#Copyright (c) 2015 Objectivity Bespoke Software Specialists

#Permission is hereby granted, free of charge, to any person obtaining a copy
#of this software and associated documentation files (the "Software"), to deal
#in the Software without restriction, including without limitation the rights
#to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
#copies of the Software, and to permit persons to whom the Software is
#furnished to do so, subject to the following conditions:

#The above copyright notice and this permission notice shall be included in all
#copies or substantial portions of the Software.

#THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
#IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
#FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
#AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
#LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
#OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
#SOFTWARE.

Feature: Unleashed Application

Background: 
	Given I log on

Scenario: Create a new product
	Given I navigate to Add Product page
	When I create a new product with mandatory details
	Then Valid successful message is displayed

	
Scenario: Complete Sales Order form and verify the available stock on hand
	Given I navigate to Add Quote page
	And I create a quote of product code "CHAIR" with 10 qty for customer code "GAR123" 
	And Customer accepts the quote
	When I complete a Sales Order from the quote
	Then I navigate to View Products page
	And Stock on hand is reduced by sold quantity
