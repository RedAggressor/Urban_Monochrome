import cl from './footer.module.scss';
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
import {
  NavLinks,
  NavLinksOrigin,
} from '../../shared/ui-kit/NavLinks/NavLinks';

export const Footer = () => {
  return (
    <footer className={cl.footer}>
      <div className={cl.footer__header}>
        <a href="/" className={`icon ${cl.footer_logo}`}>
          <img src="/logo.png" alt="Company Logo" />
        </a>
        <h3 className={cl.title_footer}>URBAN MONOCHROME</h3>
      </div>

      <div className={cl.footer_icons}>
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
        <a
          href="tel:+380(555) 123-4567"
          className={`icon ${cl.icon_hidden_desktop}`}
        >
          <PhoneIcon />
        </a>
        <a
          href="mailto:UBm@gmail.com"
          className={`icon ${cl.icon_hidden_desktop}`}
        >
          <EmailIcon />
        </a>
      </div>

      <div className={cl.contacts_desktop}>
        <p className={cl.contact}>(555) 123-4567</p>
        <p className={cl.contact}>(555) 133-4587</p>
        <a
          className={`${cl.contact} ${cl.footer_email}`}
          href="mailto:UBm@gmail.com"
        >
          UBm@gmail.com
        </a>
      </div>

      <div className={cl.footer_subscription}>
        <p className={cl.updates}>[Keep up to date]</p>
        <p className={cl.subscribe}>
          Subscribe to get the latest on new arrivals, exclusive
          <br />
          offers, and style update
        </p>
        <SearchForm placeholder={'Your email'} icon={<MailingIcon />} />
        <p className={cl.copyright_info}>
          © 2024 Glossier. All rights reserved. .{' '}
          <span className={cl.privacy}>Privacy Policy</span>
        </p>
      </div>

      <div className={cl.payment_icons}>
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

      <NavLinks
        className={`footer__nav ${cl.footer__nav}`}
        origin={NavLinksOrigin.Footer}
      />
    </footer>
  );
};
