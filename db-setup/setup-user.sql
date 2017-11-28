DO
$do$
BEGIN
  IF EXISTS(SELECT * FROM pg_user WHERE usename='chatbotuser') THEN
    REVOKE ALL ON DATABASE chatbot FROM chatbotuser;
	DROP USER chatbotuser;
  END IF;
  
  CREATE USER chatbotuser WITH PASSWORD 'securePassword';
  GRANT ALL ON DATABASE chatbot TO chatbotuser;
END
$do$