# Email Spam Filter

<div align="center">
<img src="https://cdn.freebiesupply.com/logos/thumbs/2x/uncw-logo.png" width="300" height="300" align="right" >
</div>

## Overview

This is my final project for CSC-424: Security Tools Engineering. The project revolved around creating a program to
simulate an email spam filter that targets advertisement emails using three specific techniques.

## Techniques

### Signature-based Detection

This technique was implemented by caching a collection of user-specified spam keywords and checking each email for
occurrences of these keywords based on their hash values regardless of case formatting. If the email contains any of
these keywords, in any form, uppercase, lowercase, or mixed case, the email is flagged as spam.

### Link Analysis

This technique was implemented by first extracting all links from the email. Once the links are extracted, they are then
processed by a link safety checker, which currently has one concrete implementation using the Google Safe Browsing API.
The Google Safe Browsing API link safety checker uses the following threat types to determine if a link is safe:

- Malware
- Social Engineering
- Unwanted Software
- Potentially Harmful Application

These four are used to determine if a link is safe or potentially malicious. If the Google Safe Browsing API returns
at least one of these threat types, the link is flagged as spam. Otherwise, when no threat types are returned, the link
is considered safe. If the email contains any unsafe links, it is flagged as spam.

### Unsubscribe Link Detection

This technique was implemented by first extracting all links from the email. Once the links are extracted, they are then
processed by an unsubscribe link detector, which checks if the link contains the word "unsubscribe" somewhere within the
link. Based on the project requirements, if the email does not contain an unsubscribe link, it is flagged as spam.

## Usage

In order to use this console application, you will need to obtain a Google Safe Browsing API key. The API key must be
stored in the `Secrets.json` file located in the root directory of the project with the following format:

```json
{
	"googleApiKey": "API_KEY"
}
```

The spam keywords file, `SpamKeywords.txt`, can be modified to specfiy the keywords you would like to use
for signature-based detection. The keywords should be separated by a newline with one keyword per line.

To meet the project requirements, emails are stored as text files in the `Emails` directory For simplicity, the emails
are in the following format:

```plaintext
[Subject]
[Body]
```

Where `[Subject]` is the entire first line of the text file containing the email's subject line and `[Body]` are lines
two through the end of the file containing the email's body. This format is expected to be followed for all emails in
the `Emails` directory.

To run the application, simply execute the `EmailSpamFilter.exe` file. If you clone and build the project yourself,
make sure to copy the `Emails` directory, `SpamKeywords.txt`, and `Secrets.json` to the output directory where the
executable is located before trying to run the application in your development environment.