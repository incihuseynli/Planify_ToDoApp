# Planify | Todo App 

This is a todo app example created as Windows Form project with C# , ADO.NET and SQL

## Table of contents

- [Overview](#overview)
  - [Screenshot](#screenshot)
  - [Installation](#installation)
- [Contributing](#contributing)
- [License](#license)

## Overview

### Screenshot

![](./assets/ScreentShoots/screenshoot1.png)
![](./assets/ScreentShoots/screenshoot2.png)

### Installation

###How to run this project in your own PC:
1. Clone the repo:

```bash
git clone https://github.com/incihuseynli/Planify_ToDoApp.git
```
2. Open the solution file in Visual Studio.
3. Go to Tools => NuGet Package Manager => Manages NuGet Packages For Solution
4. Download this package of Microsoft:
```
  System.Data.SqlClient
```
5. Start connection in Sql Server Management Tools
6. Replace the SQL server name in the connection string with yours if necessary
```
  "Server=.;Database=todoExampleDb;Integrated Security=true";
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
