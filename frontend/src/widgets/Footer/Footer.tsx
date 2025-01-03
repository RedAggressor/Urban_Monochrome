import '../../widgets/Footer/footer.scss';
import '../../app/styles/icons.scss';

import { EmailIcon } from '../../shared/icons/icons_footer/EmailIcon';
import { FacebookIcon } from '../../shared/icons/icons_footer/FacebookIcon';
import { InstagramIcon } from '../../shared/icons/icons_footer/InstagramIcon';
import { PhoneIcon } from '../../shared/icons/icons_footer/PhoneIcon';
import { PinterestIcon } from '../../shared/icons/icons_footer/PinterestIcon';
import { TwitterIcon } from '../../shared/icons/icons_footer/TwitterIcon';
import { SearchForm } from '../../shared/ui-kit/SearchForm/SearchForm';
import { MailingIcon } from '../../shared/icons/icons_footer/MailingIcon';
import { BankCardIcon } from '../../shared/icons/icons_footer/BankCardIcon';
import { PayPalIcon } from '../../shared/icons/icons_footer/PayPalIcon';
import { GooglePayIcon } from '../../shared/icons/icons_footer/GooglePayIcon';
import { ApplePayIcon } from '../../shared/icons/icons_footer/ApplePayIcon';
import { NavLinks } from '../../shared/ui-kit/NavLinks/NavLinks';

export const Footer = () => {
  return (
    <footer className="footer">
      <div className="footer__header">
        <a href="/" className="icon footer_logo">
          <img src="/logo.png" alt="Company Logo" />
        </a>
        <h3 className="title_footer">URBAN MONOCHROME</h3>
      </div>

      <div className="footer_icons">
        <a href="#" className="icon">
          <InstagramIcon />
        </a>
        <a href="#" className="icon">
          <PinterestIcon />
        </a>
        <a href="#" className="icon">
          <FacebookIcon />
        </a>
        <a href="#" className="icon">
          <TwitterIcon />
        </a>
        <a href="tel:+380(555) 123-4567" className="icon icon-hidden-desktop">
          <PhoneIcon />
        </a>
        <a href="mailto:UBm@gmail.com" className="icon icon-hidden-desktop">
          <EmailIcon />
        </a>
      </div>

      <div className="contacts-desktop">
        <p className="contact">(555) 123-4567</p>
        <p className="contact">(555) 133-4587</p>
        <a className='contact footer_email' href="mailto:UBm@gmail.com">UBm@gmail.com</a>
      </div>

      <div className="footer_subscription">
        <p className="updates">[Keep up to date]</p>
        <p className="subscribe">Subscribe to get the latest on new arrivals, exclusive<br />offers, and style update</p>
        <SearchForm placeholder={"Your email"} icon={<MailingIcon />} />
        <p className="copyright-info">© 2024 Glossier. All rights reserved. . <span className='privacy'>Privacy Policy</span></p>
      </div>

      <div className="payment-icons">
        <a href="#" className="icon">
          <BankCardIcon />
        </a>
        <a href="#" className="icon">
          <PayPalIcon />
        </a>
        <a href="#" className="icon">
          <GooglePayIcon />
        </a>
        <a href="#" className="icon">
          <ApplePayIcon />
        </a>
        </div>

        <NavLinks className='footer__nav' />
    </footer>
  );
};
