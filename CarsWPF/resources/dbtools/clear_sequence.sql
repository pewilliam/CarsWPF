DO $$
DECLARE
i TEXT;
BEGIN
 FOR i IN (SELECT column_default FROM information_schema.columns WHERE column_default SIMILAR TO 'nextval%') 
  LOOP
         EXECUTE 'ALTER SEQUENCE'||' ' || substring(substring(i from '''[a-z_]*')from '[a-z_]+') || ' '||' RESTART 1;';    
  END LOOP;
END $$; 