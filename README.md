## C# Exercise:

### Problem-I


          Given two sorted files, write a C# program to merge them while preserving sort order.
          The program should determine what data type is used in input files (DateTime, Double, String in
          that sequence) and merge them accordingly.
          DO NOT assume either of these files can fit in memory.
### How to Run:

          git clone <<url>>
          cd dsexercise/Q1
          dotnet restore
          dotnet build
          dotnet test
          dotnet run

#### Test Cases


          #1:
          File1 content:
          aa
          ac
          ad
          az

          File2 content:
          ab
          ae
          ak
          am

          output:
          Find Merged_File.txt in temp directory ( Window+R then %temp% + Enter) 

          aa
          ab
          ac
          ad
          ae
          ak
          am
          az

          #2:
          File1 content:
          01/01/1987
          01/01/1987
          01/01/1988
          01/01/1989
          01/02/1989

          File2 content:
          01/01/1983
          01/01/1987
          01/02/1988
          01/04/1989
          01/05/1989

          Output:
          01/01/1983
          01/01/1987
          01/01/1987
          01/01/1987
          01/01/1988
          01/02/1988
          01/01/1989
          01/02/1989
          01/04/1989
          01/05/1989




### Problem-II:


          You are given a file formatted like this:
          CUSIP
          Price
          Price
          Price
          …
          CUSIP
          Price
          Price
          CUSIP
          Price
          Price
          Price
          …
          Price
          CUSIP
          Price
          …

          Think of it as a file of price ticks for a set of bonds identified by their CUSIPs.
          You can assume a CUSIP is just an 8-character alphanumeric string. Each CUSIP may have any
          number of prices (e.g., 95.752, 101.255) following it in sequence, one per line.
          The prices can be considered to be ordered by time in ascending order, earliest to latest.
          Write a C# program that will print the opening, lowest, highest, closing price for each CUSIP in
          the file.
          DO NOT assume the entire file can fit in memory.

### How to Run:

          git clone <<url>>
          cd dsexercise/Q2
          dotnet restore
          dotnet build
          dotnet test
          dotnet run
          
#### Test Cases


          #1:
          CUSIP-1
          11.11
          10.05
          20.10
          10.05
          30.15
          10.05
          40.20
          33.33
          CUSIP-II
          22.22
          10.05
          20.10
          10.05
          30.15
          10.05
          40.20
          99.99

          output:
          -------------------
          Cusip - CUSIP-1
          Lowest - 10.05
          Highest - 40.2
          Opening - 11.11
          Closing - 33.33
          -------------------
          -------------------
          Cusip - CUSIP-II
          Lowest - 10.05
          Highest - 99.99
          Opening - 22.22
          Closing - 99.99
          -------------------
          Complete!!

#### Q2 solution with TPL Dataflow library (with pipeline pattern)

### How to Run:

          git clone <<url>>
          cd dsexercise/Q2.DataPipeline
          dotnet restore
          dotnet build
          dotnet test
          dotnet run