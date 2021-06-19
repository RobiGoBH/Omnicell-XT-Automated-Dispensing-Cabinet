Omnicell [RemoteInterview.io] Programing test.

Description:

You have been tasked to create the internal logic for the next generation Omnicell XT Automated Dispensing Cabinet. 

This cabinet will store and track all medications for a given clinic.  Here is what the cabinet looks like:

https://www.omnicell.co.uk/US/Products/CP%20Dispense/PoC002%20XT%20Automated%20Dispensing%20Cabinets_1480.jpg

The cabinet has a series of locations or "bins" where medications are stored.  A drawer bin looks like this:

https://www.pocketnurse.com/media/catalog/product/cache/46c67a7e014cd6344be7c367252caa28/0/4/04-37-1655_2.jpg

Please write the control software to support the following requirements:  

The cabinet will have 10 different locations with 3 different size bins to store medications.
"large" - 2 bins - stores "15 units" 
"medium" - 5 bins - stores "10 units" 
"small" - 3 bins" - stores "5 units"
The user will specify what is stored in each bin.   They will need to maintain the following attributes: 
Medication ID (NDC)
Medication Name
A user will be able to add or remove medication units from a bin
Bins can be reset to support different medications based on customer needs. 
System must track what each specific users adds/removes from a cabinet (for compliance purposes)
System must be able to report out what medications need to be "reordered" based on low inventory (under 20% capacity) and recent medication usage.
Handle as many relevant edge cases as realistically possible.  

Write a scenario test to validate the functionality.  Write unit tests where feasible.

C# or Javascript is preferred but use which language is best for you.  