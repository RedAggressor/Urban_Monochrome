import cl from './footer.module.scss';

import {
  NavLinks,
  NavLinksOrigin,
} from '../../shared/ui-kit/NavLinks/NavLinks';
import { Logo } from '../../shared/ui-kit/Logo/Logo';
import { Icon, IconType } from '../../shared/ui-kit/Icon/Icon';
import { useAppSelector } from '../../shared/hooks';
import { Container } from '../../shared/Container/Container';

export const Footer = () => {
  const { screenWidth } = useAppSelector(st => st.global);
  // на малих екранах структура кардинально різна тому простіше так написати
  if (screenWidth < 1024) {
    return (
      <footer>
        <Container className={`${cl.footer} ${cl.footer_mob}`}>
          <div className={cl.logoTitle}>
            <Logo />
            <h1 className={cl.logoTitle__title}>URBAN MONOCHROME</h1>
          </div>

          <div className={cl.iconsContainer}>
            <Icon type={IconType.Instagram} />
            <Icon type={IconType.Pinterest} />
            <Icon type={IconType.Facebook} />
            <Icon type={IconType.Twitter} />
            <Icon type={IconType.Phone} />
            <Icon type={IconType.Email} />
          </div>

          <section className={cl.upToDateContainer}>
            <p className={cl.upToDateContainer_title}>[Keep up to date]</p>
            <p className={cl.upToDateContainer_text}>
              Subscribe to get the latest on new arrivals, exclusive offers and
              style update
            </p>
            <form
              action=""
              className={cl.upToDateContainer_form}
              onSubmit={e => e.preventDefault()}
            >
              <input
                type="email"
                className={cl.upToDateContainer_form__input}
                placeholder="Your email"
              />
              <Icon
                type={IconType.MailArrow}
                className={cl.upToDateContainer_form__icon}
                onClick={() => {}}
              />
            </form>
          </section>

          <section className={cl.bottomContainer}>
            <p className={cl.bottomContainer__info}>
              © 2024 Glossier. All rights reserved. .{' '}
              <a href="">
                <u>Privacy Policy</u>
              </a>
            </p>
            <div className={cl.bottomContainer__paymentIcons}>
              <Icon type={IconType.BankCard} size="small" />
              <Icon type={IconType.PayPal} size="small" />
              <Icon type={IconType.GooglePay} size="small" />
              <Icon type={IconType.ApplePay} size="small" />
            </div>
          </section>
        </Container>
      </footer>
    );
  }

  return (
    <footer>
      <Container className={`${cl.footer} ${cl.footer_desk}`}>
        <div className={cl.desk_sectionContainer}>
          <div className={cl.logoTitle}>
            <Logo />
            <h1 className={cl.logoTitle__title}>URBAN MONOCHROME</h1>
          </div>
          <div className={cl.iconsContainer}>
            <Icon type={IconType.Instagram} />
            <Icon type={IconType.Pinterest} />
            <Icon type={IconType.Facebook} />
            <Icon type={IconType.Twitter} />
          </div>
        </div>

        <div className={cl.desk_sectionContainer}>
          <section className={cl.upToDateContainer}>
            <p className={cl.upToDateContainer_title}>[Keep up to date]</p>
            <p className={cl.upToDateContainer_text}>
              Subscribe to get the latest on new arrivals, exclusive offers and
              style update
            </p>
            <form
              action=""
              className={cl.upToDateContainer_form}
              onSubmit={e => e.preventDefault()}
            >
              <input
                type="email"
                className={cl.upToDateContainer_form__input}
                placeholder="Your email"
              />
              <Icon
                type={IconType.MailArrow}
                className={cl.upToDateContainer_form__icon}
                onClick={() => {}}
              />
            </form>
          </section>
          <NavLinks origin={NavLinksOrigin.Footer} className={cl.desk_nav} />
          <address className={cl.desk_contacts}>
            <a href="tel:+123-4567">(555) 123-4567</a>
            <a href="tel:+133-4587">(555) 133-4587</a>
            <a href="mailto:UBm@gmail.com">UBm@gmail.com</a>
          </address>
        </div>

        <div className={cl.bottomContainer}>
          <p className={cl.bottomContainer__info}>
            © 2024 Glossier. All rights reserved. .{' '}
            <a href="">
              <u>Privacy Policy</u>
            </a>
          </p>
          <div className={cl.bottomContainer__paymentIcons}>
            <Icon type={IconType.BankCard} />
            <Icon type={IconType.PayPal} />
            <Icon type={IconType.GooglePay} />
            <Icon type={IconType.ApplePay} />
          </div>
        </div>
      </Container>
    </footer>
  );
};
