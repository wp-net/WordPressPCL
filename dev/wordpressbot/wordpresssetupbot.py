from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.common.action_chains import ActionChains

options = webdriver.ChromeOptions()
browser = webdriver.Remote(command_executor="http://selenium:4444/wd/hub", options=options)
print("Browser driver initialized")

browser.get("http://wordpress")
print("Launching the wordpress page in browser")

#here the interaction with browser starts

#language selection
browser.find_element(By.ID, "language-continue").click()
print("Language selection done")

#Installation form completion
username = password = "wordpress"

browser.find_element(By.NAME, "weblog_title").send_keys("test")
browser.find_element(By.NAME, "user_name").send_keys(username)

password_element = browser.find_element(By.NAME, "admin_password")
password_element.clear()
password_element.send_keys(password)

browser.find_element(By.NAME, "pw_weak").click()
browser.find_element(By.NAME, "admin_email").send_keys("test@example.com")
browser.find_element(By.NAME, "blog_public").click()

browser.find_element(By.ID, "submit").click()
print("Wordpress user setup done")

#Log in
browser.find_element(By.XPATH, "/html/body/p[3]/a").click()
browser.find_element(By.NAME, "log").send_keys(username)
browser.find_element(By.NAME, "pwd").send_keys(password)
browser.find_element(By.ID, "wp-submit").click()
print("Wordpress user logged in")

#Settings for wordpress instance
menu_settings = browser.find_element(By.XPATH, "//*[@id=\"menu-settings\"]/a/div[3]")
hover = ActionChains(browser).move_to_element(menu_settings)
hover.perform()
browser.find_element(By.XPATH, "//*[@id=\"menu-settings\"]/ul/li[7]/a").click()
browser.find_element(By.XPATH, "//*[@id=\"wpbody-content\"]/div[3]/form/table[1]/tbody/tr[5]/th/label/input").click()
browser.find_element(By.ID, "submit").click()
print("Wordpress settings updated")

#Configuring wordpress plugins
menu_plugins = browser.find_element(By.XPATH, "//*[@id=\"menu-plugins\"]/a/div[3]")
hover = ActionChains(browser).move_to_element(menu_plugins)
hover.perform()
browser.find_element(By.XPATH, "//*[@id=\"menu-plugins\"]/ul/li[2]/a").click()
browser.find_element(By.ID, "activate-enable-application-passwords").click()

browser.find_element(By.XPATH, "//*[@id=\"wpbody-content\"]/div[3]/a").click()
browser.find_element(By.ID, "search-plugins").send_keys("contact form 7")
browser.implicitly_wait(8)
contact_form_plugin_card = browser.find_element(By.CLASS_NAME, "plugin-card-contact-form-7")
contact_form_plugin_card.find_element(By.CLASS_NAME, "install-now").click()
browser.implicitly_wait(8)
contact_form_plugin_card.find_element(By.CLASS_NAME, "activate-now").click()

browser.find_element(By.XPATH, "//*[@id=\"wpbody-content\"]/div[3]/a").click()
browser.find_element(By.ID, "search-plugins").send_keys("jwt-authentication for wp-api")
browser.implicitly_wait(8)
jwt_auth_plugin_card = browser.find_element(By.CLASS_NAME, "plugin-card-jwt-authentication-for-wp-rest-api")
jwt_auth_plugin_card.find_element(By.CLASS_NAME, "install-now").click()
browser.implicitly_wait(8)
jwt_auth_plugin_card.find_element(By.CLASS_NAME, "activate-now").click()
print("Wordpress plugins configured")

#cleanup
browser.quit()
print("Wordpress setup done")