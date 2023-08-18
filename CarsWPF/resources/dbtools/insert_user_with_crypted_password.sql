INSERT INTO user_account (user_name, password_hash) 
VALUES ('user1', crypt('user1_password', gen_salt('md5'))) ;