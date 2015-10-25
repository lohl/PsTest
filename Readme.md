# Coding Test

The purpose of this coding test is to see how you solve a couple of problems. There is no "right" answer but we are curious to see how you answer the problems.

Included in this project is an ASP.net MVC project 

1. We just added a new field called TotalMag which is calculated by adding up the fields MagX, MagY and MagZ. Add this new field TotalMag to the various views for readings.
2. In addition to these mag fields we would like to add Grav fields which follow the same pattern as the Mag fields. That is that there are 3 base fields
 - GravX
 - GravY
 - GravZ
 And also a total field which is calcualted using the formula. 

 ```GravTotal = (GravX + GravY)/GravZ```

 The ranges on the Grav fields should be validated. 


3. Ensure that records on the index page are sorted in accordance with depth order

4. Add a confirmation message to assure users that when they enter a new record it has been saved

Code should be submitted back as a PR. Please don't squash your commits. 