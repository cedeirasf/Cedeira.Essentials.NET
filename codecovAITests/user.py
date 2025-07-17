class User:
    def __init__(self, username):
        self.username = username
        self.logged_in = False

    def login(self, password):
        if password == "secret":
            self.logged_in = True
        return self.logged_in

    def logout(self):
        self.logged_in = False

    def is_authenticated(self):
        return self.logged_in
