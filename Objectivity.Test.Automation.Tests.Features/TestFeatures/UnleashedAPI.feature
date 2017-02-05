
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

Feature: Unleashed API

@API
Scenario: create a new customer
	Given I post a new customer details
	When I get customer details
	Then Valid customer code is returned

@API
Scenario: create a new customer with duplicated GUID
	Given I post a new customer details
	When I post a new customer details again with same GUID
	Then Bad response is returned with duplicated Guid error message

@API
Scenario: create a new customer with duplicated customer code
	Given I post a new customer details
	When I post a new customer details again with same CustomerCode
	Then Bad response is returned with duplicated customer code error message