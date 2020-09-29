CREATE FUNCTION [dbo].[CalcFullAge]
(
    @BirthOfDate Date
)
RETURNS int AS
BEGIN
    RETURN ((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),@BirthOfDate,112))/10000)
END