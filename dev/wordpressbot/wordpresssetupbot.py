from selenium import webdriver
from selenium.webdriver.common.action_chains import ActionChains

options = webdriver.ChromeOptions()
browser = webdriver.Remote(command_executor="http://selenium:4444/wd/hub", options=options)
print("Browser driver initialized")

browser.get("http://wordpress")
print("Launching the wordpress page in browser")

#here the interaction with browser starts

#language selection
browser.find_element_by_id("language-continue").click()
print("Language selection done")

#Installation form completion
username = password = "wordpress"

browser.find_element_by_name("weblog_title").send_keys("test")
browser.find_element_by_name("user_name").send_keys(username)

password_element = browser.find_element_by_name("admin_password")
password_element.clear()
password_element.send_keys(password)

browser.find_element_by_name("pw_weak").click()
browser.find_element_by_name("admin_email").send_keys("test@example.com")
browser.find_element_by_name("blog_public").click()

browser.find_element_by_id("submit").click()
print("Wordpress user setup done")

#Log in
browser.find_element_by_xpath("/html/body/p[3]/a").click()
browser.find_element_by_name("log").send_keys(username)
browser.find_element_by_name("pwd").send_keys(password)
browser.find_element_by_id("wp-submit").click()
print("Wordpress user logged in")

#Settings for wordpress instance
menu_settings = browser.find_element_by_xpath("//*[@id=\"menu-settings\"]/a/div[3]")
hover = ActionChains(browser).move_to_element(menu_settings)
hover.perform()
browser.find_element_by_xpath("//*[@id=\"menu-settings\"]/ul/li[7]/a").click()
browser.find_element_by_xpath("//*[@id=\"wpbody-content\"]/div[3]/form/table[1]/tbody/tr[5]/th/label/input").click()
browser.find_element_by_id("submit").click()
print("Wordpress settings updated")

#Configuring wordpress plugins
menu_plugins = browser.find_element_by_xpath("//*[@id=\"menu-plugins\"]/a/div[3]")
hover = ActionChains(browser).move_to_element(menu_plugins)
hover.perform()
browser.find_element_by_xpath("//*[@id=\"menu-plugins\"]/ul/li[2]/a").click()
browser.find_element_by_id("activate-enable-application-passwords").click()

browser.find_element_by_xpath("//*[@id=\"wpbody-content\"]/div[3]/a").click()
browser.find_element_by_id("search-plugins").send_keys("contact form 7")
browser.implicitly_wait(8)
contact_form_plugin_card = browser.find_element_by_class_name("plugin-card-contact-form-7")
contact_form_plugin_card.find_element_by_class_name("install-now").click()
browser.implicitly_wait(8)
contact_form_plugin_card.find_element_by_class_name("activate-now").click()

browser.find_element_by_xpath("//*[@id=\"wpbody-content\"]/div[3]/a").click()
browser.find_element_by_id("search-plugins").send_keys("jwt-authentication for wp-api")
browser.implicitly_wait(8)
jwt_auth_plugin_card = browser.find_element_by_class_name("plugin-card-jwt-authentication-for-wp-rest-api")
jwt_auth_plugin_card.find_element_by_class_name("install-now").click()
browser.implicitly_wait(8)
jwt_auth_plugin_card.find_element_by_class_name("activate-now").click()
print("Wordpress plugins configured")

#cleanup
browser.quit()
print("Wordpress setup done")