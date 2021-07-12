# John.Edward.Santillan
Planit Assessment - Automation Test

1. What other possible scenario’s would you suggest for testing the Jupiter Toys application?

	- Test the Login popup page
	- Additional test in Cart page
		a. Test Quantity text box by updating field value and validate Subtotal's value 
		b. Click Remove Item button under Actions column. Validate if product in that row will be removed from Cart page
		c. Click Empty Cart button to validate if all products added in Cart page are removed
		d. Validate Check Out button and test how the button will be disabled and re-enabled
	- Test the Check Out page
		a. Error validations for mandatory fields of both Delivery Details and Payment Details section
		b. Email format validation of Email text box
		c.Test Submit button and verify if successfully submitted
		
2. Jupiter Toys is expected to grow and expand its offering into books, tech, and modern art. We are expecting the of tests will grow to a very large number.
	• What approaches could you use to reduce overall execution time?
		- Use of headless test execution
		- Segregate test cases such that each group of tests are independent from each other. By doing this, we can implement parallel test execution where each group are assigned to different machine
		
	• How will your framework cater for this?
		- The framework I made should be able to cater to the application's growth as code reusability and extensibility were considered when framework was built. Each page has its corresponding page object where each element and functionalities are specific to what that page can do. It is able to inherit from a base page to prevent code duplication.

3. Describe when to use a BDD approach to automation and when NOT to use BDD 

- BDD approach in automation should be used for the following instances:
	a. Ideally for mid to large projects where project scope, specification and user stories are well defined and BAs, QAs, Automation Testers and Developers can closely collaborate with each other to define behavior of the application and plan steps before it gets developed and automated as BDD is highly collaborative. If this can't be done, BDD will fail
	b. If the objective is end-to-end tests (for instance, from user login, user will do other business functions until user logout)
	c. For UI and browser testing
	
- On the other hand, BDD should NOT be used for the following instances:
	a. For smaller projects as BDD requires an overhead investment of time and effort in planning steps and building of code
	b. If user stories are not defined as it will be hard to clearly define the behavior of the application
	c. If application frequently changes specially its existing business functions as it will be challenging for maintenance and cost
	d. For API testing
