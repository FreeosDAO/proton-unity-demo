var LibraryMyPlugin = {
    $Proton: {
        PROTON_ENDPOINTS: ['https://proton.greymass.com'],
        REQUEST_ACCOUNT: 'glbdex',
        CHAIN_ID: '384da888112027f0321850a169f737c33e53b388aad48b5adace4bab97f437e0',
        APP_NAME: 'GLBDEX',
        session: undefined,
        link: undefined,
        avatar: undefined,
        txResult: undefined,
    },
  
    CreateLink: async function (restoreSession = false) {
        const { link, session } = await window.ProtonWebSDK({
            linkOptions: {
                endpoints: Proton.PROTON_ENDPOINTS,
                chainId: Proton.CHAIN_ID,
                restoreSession,
            },
            transportOptions: {
                requestAccount: Proton.REQUEST_ACCOUNT,
                requestStatus: false,
                backButton: true,
            },
            selectorOptions: {
                appName: Proton.APP_NAME,
            },
        });
    
        Proton.link = link
        Proton.session = session
    },

    Login__deps: ['CreateLink'],
    Login: async function (callback) {
        await _CreateLink(false);
        dynCall_vi(callback, !!Proton.session);
    },
    
    Logout: async function () {
        if (Proton.link && Proton.session) {
          await Proton.link.removeSession(Proton.REQUEST_ACCOUNT, Proton.session.auth, Proton.CHAIN_ID);
        }
        Proton.session = undefined;
        Proton.link = undefined;
    },

    Reconnect__deps: ['CreateLink'],
    Reconnect: async function (callback) {
        if (!Proton.session) {
          await _CreateLink(true);
        }
        dynCall_vi(callback, !!Proton.session);
    },

    Transfer__deps: ['StringToBuffer'],
    Transfer: async function (to, amount, callback) {
        if (!Proton.session) {
          throw new Error('No Session');
        }

        /**
        * The token contract, precision and symbol for tokens can
        * be seen at protonscan.io/tokens
        */

        const tokenContract = "eosio.token"
        const tokenPrecision = 4
        const tokenSymbol = "XPR"
      
        Proton.txResult = await Proton.session.transact({
          actions: [{
            // Token contract
            account: tokenContract,
      
            // Action name
            name: "transfer",
            
            // Action parameters
            data: {
              // Sender
              from: Proton.session.auth.actor,
      
              // Receiver
              to: UTF8ToString(to),

              // Amount to precision, and symbol
              quantity: `${amount.toFixed(tokenPrecision)} ${tokenSymbol}`,
      
              // Optional memo
              memo: "Sent from Unity"
            },
            authorization: [Proton.session.auth]
          }]
        }, {
          broadcast: true
        })

        dynCall_vi(callback, _StringToBuffer(Proton.txResult.processed.id))
    },

    Transact: async function (
        actions,
        broadcast
    ) {
        if (Proton.session) {
            Proton.txResult = await Proton.session.transact(
                {
                    transaction: {
                        actions,
                    },
                },
                { broadcast }
            );
        } else {
          throw new Error('No Session');
        }
    },

    GetProtonAvatar: async function (account) {
        try {
          const result = await Proton.link.client.get_table_rows({
            code: 'eosio.proton',
            scope: 'eosio.proton',
            table: 'usersinfo',
            key_type: 'i64',
            lower_bound: account,
            index_position: 1,
            limit: 1
          })
      
          if (result.rows.length > 0 && result.rows[0].acc === account) {
            Proton.avatar = result.rows[0]
          }
        } catch (e) {
          console.error('getProtonAvatar error', e)
        }
      
        Proton.avatar = undefined
    },

    StringToBuffer: function (str) {
        var bufferSize = lengthBytesUTF8(str) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(str, buffer, bufferSize);
        return buffer;
    },
};
  
autoAddDeps(LibraryMyPlugin, '$Proton');
mergeInto(LibraryManager.library, LibraryMyPlugin);