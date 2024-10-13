
</head>
<body>

<h1>IKEA Project</h1>

<h2>Overview</h2>
<p>IKEA is a simple ASP.NET Core MVC project that allows authenticated users to perform CRUD operations on departments and employees.</p>

<h2>Table of Contents</h2>
<ul>
    <li><a href="#getting-started">Getting Started</a></li>
    <li><a href="#features">Features</a>
        <ul>
            <li><a href="#authentication">Authentication</a></li>
            <li><a href="#department-management">Department Management</a></li>
            <li><a href="#employee-management">Employee Management</a></li>
        </ul>
    </li>
    <li><a href="#modules">Modules</a>
        <ul>
            <li><a href="#user-module">User Module</a></li>
            <li><a href="#department-module">Department Module</a></li>
            <li><a href="#employee-module">Employee Module</a></li>
        </ul>
    </li>
    <li><a href="#technologies-used">Technologies Used</a></li>
    <li><a href="#installation">Installation</a></li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
</ul>

<h2 id="getting-started">Getting Started</h2>
<p>Follow these instructions to set up the project locally.</p>

<h2 id="features">Features</h2>

<h3 id="authentication">Authentication</h3>
<ul>
    <li>User registration and login</li>
    <li>Password hashing and security measures</li>
    <li>Role-based access control</li>
</ul>

<h3 id="department-management">Department Management</h3>
<ul>
    <li>Create, read, update, and delete (CRUD) operations for departments</li>
    <li>Department listing and detailed views</li>
</ul>

<h3 id="employee-management">Employee Management</h3>
<ul>
    <li>CRUD operations for employees</li>
    <li>Employee listing and detailed views</li>
    <li>Association of employees with departments</li>
</ul>

<h2 id="modules">Modules</h2>

<h3 id="user-module">User Module</h3>
<p>Handles user authentication and authorization.</p>

<h4>Features:</h4>
<ul>
    <li>User Registration</li>
    <li>User Login</li>
    <li>Password Encryption</li>
    <li>JWT Token Generation</li>
</ul>

<h3 id="department-module">Department Module</h3>
<p>Manages department-related operations.</p>

<h4>Features:</h4>
<ul>
    <li>Add New Department</li>
    <li>View Department Details</li>
    <li>Edit Department Information</li>
    <li>Delete Department</li>
</ul>

<h3 id="employee-module">Employee Module</h3>
<p>Manages employee-related operations.</p>

<h4>Features:</h4>
<ul>
    <li>Add New Employee</li>
    <li>View Employee Details</li>
    <li>Edit Employee Information</li>
    <li>Delete Employee</li>
    <li>Assign Employee to Department</li>
</ul>

<h2 id="technologies-used">Technologies Used</h2>
<ul>
    <li>ASP.NET Core MVC</li>
    <li>Entity Framework Core</li>
    <li>SQL Server</li>
    <li>Bootstrap</li>
</ul>

<h2 id="installation">Installation</h2>
<ol>
    <li>Clone the repository:
        <pre><code>git clone https://github.com/rawwaanntarekk/IKEA.git</code></pre>
    </li>
    <li>Navigate to the project directory:
        <pre><code>cd IKEA</code></pre>
    </li>
    <li>Install the dependencies:
        <pre><code>dotnet restore</code></pre>
    </li>
</ol>

<h2 id="usage">Usage</h2>
<ol>
    <li>Update the database connection string in <code>appsettings.json</code>.</li>
    <li>Run the migrations to create the database:
        <pre><code>dotnet ef database update</code></pre>
    </li>
    <li>Start the application:
        <pre><code>dotnet run</code></pre>
    </li>
</ol>

<h2 id="contributing">Contributing</h2>
<p>Contributions are welcome! Please read the <code>contributing guidelines</code> before submitting a pull request.</p>

<h2 id="license">License</h2>
<p>This project is licensed under the MIT License.</p>

</body>
</html>
