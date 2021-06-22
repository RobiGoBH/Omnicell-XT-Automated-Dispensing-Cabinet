<p><strong>﻿# Omnicell-XT-Automated-Dispensing-Cabinet</strong></p>
<h3>Omnicell [RemoteInterview.io] Programing test.</h3>

NOTE: This solution was made to be able to copy and paste back into [RemoteInterview.io] single page editor.
I will do another project within this solution and split things properly in their own file as this single file solution is ugly as is limited to one block of code, and that limitation is not present in production environment. 

<p><strong>Description:</strong></p>

<p>You have been tasked to create the internal logic for the next generation Omnicell XT Automated Dispensing Cabinet. </p>

<p>This cabinet will store and track all medications for a given clinic.  Here is what the cabinet looks like:</p>

<p><img src="https://www.omnicell.co.uk/US/Products/CP%20Dispense/PoC002%20XT%20Automated%20Dispensing%20Cabinets_1480.jpg" alt=”Omnicell Cabinet”></p>

<p>The cabinet has a series of locations or "bins" where medications are stored.  A drawer bin looks like this:</p>

<p><img src="https://www.pocketnurse.com/media/catalog/product/cache/46c67a7e014cd6344be7c367252caa28/0/4/04-37-1655_2.jpg" alt=”Drawer Bin”></p>

<p><strong>Please write the control software to support the following requirements:</strong></p> 

<p>The cabinet will have 10 different locations with 3 different size bins to store medications.</p>
<ul>
  <li>"large" - 2 bins - stores "15 units" </li>
  <li>"medium" - 5 bins - stores "10 units" </li>
  <li>"small" - 3 bins" - stores "5 units"</li>
</ul>
<p>The user will specify what is stored in each bin.   They will need to maintain the following attributes:</p> 
<ul>
  <li>Medication ID (NDC)</li>
  <li>Medication Name</li>
</ul>
<p>A user will be able to add or remove medication units from a bin<br>
Bins can be reset to support different medications based on customer needs.<br> 
System must track what each specific users adds/removes from a cabinet (for compliance purposes)<br>
System must be able to report out what medications need to be "reordered" based on low inventory (under 20% capacity) and recent medication usage.<br>
Handle as many relevant edge cases as realistically possible.</p>  

<p>Write a scenario test to validate the functionality.  Write unit tests where feasible.</p>

<p>C# or Javascript is preferred but use which language is best for you.</p>
