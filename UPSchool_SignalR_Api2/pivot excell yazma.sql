select tarih,[1],[2],[3],[4],[5] from (select [City],[Count],cast([ElectricDate] as date) as tarih from Elektriks) as electricT pivot(sum(count) for city in([1],[2],[3],[4],[5]) )  as ptable order by tarih asc
truncate table Elektriks