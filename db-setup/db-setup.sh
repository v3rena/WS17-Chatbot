psql -U postgres -W -c "DROP DATABASE IF EXISTS chatbot;"
psql -U postgres -W -c "CREATE DATABASE chatbot;"
psql -U postgres -W -f "./setup-user.sql"