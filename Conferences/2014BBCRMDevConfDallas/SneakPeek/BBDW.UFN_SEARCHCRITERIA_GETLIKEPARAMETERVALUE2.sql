CREATE function BBDW.UFN_SEARCHCRITERIA_GETLIKEPARAMETERVALUE2
(
    @CRITERIA nvarchar(1000),
    @EXACTMATCHONLY bit = 0,
    @ESCAPECHAR nvarchar(1) = '\',
    @APPENDWILDCARD bit = 1
)
returns nvarchar(1000)
as
begin
  if @CRITERIA is not null
    begin
        
      if @ESCAPECHAR = '' or @ESCAPECHAR is null
        set @ESCAPECHAR = '\';
        
      set @CRITERIA = replace(@CRITERIA, @ESCAPECHAR, @ESCAPECHAR + @ESCAPECHAR);
      set @CRITERIA = replace(@CRITERIA, '[', @ESCAPECHAR + '[');
      set @CRITERIA = replace(@CRITERIA, ']', @ESCAPECHAR + ']');

      if coalesce(@EXACTMATCHONLY, 0) = 1
        begin
          set @CRITERIA = replace(@CRITERIA, '%', @ESCAPECHAR + '%');
          set @CRITERIA = replace(@CRITERIA, '_', @ESCAPECHAR + '_');
        end
      else  
        begin
          if @APPENDWILDCARD = 1
            set @CRITERIA = @CRITERIA + '%';
          set @CRITERIA = replace(@CRITERIA, '*', '%');
          set @CRITERIA = replace(@CRITERIA, '?', '_');
        end
    end
    
  return @CRITERIA;
  
  end

        
