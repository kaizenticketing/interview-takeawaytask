
# Brief
In plain business terms...

* Add a new section to customer's my account to view, add, remove and request permission to 'manage tickets'. Anyone who has allowed us as a purchasing customer to 'manage' their tickets on their behalf should be known as a 'managed' customer.
    * This *also* needs to show a separate list of customers who are 'managing' our account - with the ability for the customer to stop them from acting on their behalf. To 'break' the link.
* Allow prioritisation of these outbound 'links'? TBD.
* Maximum limit of links - 15? Even smaller number of active 'manage tickets' permission.
* Requesting permission to 'manage tickets' should send an email (with digest etc - similar to activate email) of a new notification type to ask the target customer to confirm they want us to manage their tickets
    * Copy the activation email and then iterate with rest of the team on its content
    * There will need to be a landing route/controller to confirm the digest
    * Make sure they are told that they can opt out once this is done through their F&F section
* What if they don't have an email address? On user based channels this should still be allowed without verification? DISCUSS.
* Assign UI should be changed to show a list of linked customers (left) and the existing add / lookup UI on the right
* Change the right to lookup/register then add to the purchasing customer's F&F AND then do the assignment
* Left lets you just click quickly to choose people
* Migrate existing data into this. Any account that was 'created by' another - should be added WITH management permissions?? DISCUSS. Anyone who has otherwise been an attendee where the purchasing customer purchased should be adding as a F&F without 
* Expand getting reservations (wherever that is exposed in the UI) to fetch from the purchasing customer AND all of its 'managed' customers. Need to order, group sensibly (iterate) and allow confirmation of reservations all the way through the flow
* DISCUSS. Should we allow a purchasing customer to view and perform actions on orders (intermingled in their list in a different colour?) of 'managed' customers?
	

# TODO
* ... break down the brief into engineering implementation smaller steps for review ... 

# Database Changes
* ...

    ## Manual Data Updates Required
    * ... in any environment(s) ...

# Setup Notes
* ...

# Test Notes
* ... what areas should QA look at that you might not expect ...

# Out of scope
* ...

# Bugs

## Confirmed Fixed

## For Review

## Outstanding

- [x] This is a bug summary
  *This is a repro step 1
  *Step 2
  * :screwdriver: I think this is fixed
  * :raising_hand_man: Need more information
  * :skull_and_crossbones: Not going to fix
  * :art: By Design
  * :boom: Still broken



